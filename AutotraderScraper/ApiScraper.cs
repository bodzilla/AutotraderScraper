using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using AutotraderScraper.Model;
using AutotraderScraper.Model.Api;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Model.Api.Mot;
using AutotraderScraper.Repository.Api;
using AutotraderScraper.Repository.Api.Autotrader;
using AutotraderScraper.Repository.Api.Mot;
using log4net;
using Newtonsoft.Json.Linq;

namespace AutotraderScraper
{
    internal static class ApiScraper
    {
        private static readonly ILog Log;
        private static readonly bool UseProxy;
        private static readonly bool UseSleep;
        private static readonly int SleepMin;
        private static readonly int SleepMax;
        private static readonly string ApiHistoryUrl;
        private static readonly string AutoTraderApiEndPoint;
        private static readonly Regex RemoveNonNumeric;

        private static readonly ApiArticleRepository ApiArticleRepo;

        static ApiScraper()
        {
            // Load settings.
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            UseProxy = bool.Parse(ConfigurationManager.AppSettings["UseProxyApi"]);
            UseSleep = bool.Parse(ConfigurationManager.AppSettings["UseSleepApi"]);
            SleepMin = int.Parse(ConfigurationManager.AppSettings["MinSleepMilliSecs"]);
            SleepMax = int.Parse(ConfigurationManager.AppSettings["MaxSleepMilliSecs"]);

            // Initialise objects.
            ApiHistoryUrl = ConfigurationManager.AppSettings["ApiMotHistoryUrl"];
            AutoTraderApiEndPoint = ConfigurationManager.AppSettings["ApiAutotraderEndPoint"];
            RemoveNonNumeric = new Regex(@"[^\d]");
            ApiArticleRepo = new ApiArticleRepository();
        }

        public static string Run(params Article[] articles)
        {
            string msg = String.Empty;
            MotResponseRepository motRepo = new MotResponseRepository();
            MotTestRepository motTestRepo = new MotTestRepository();
            RfrAndCommentRepository rfrAndCommentRepo = new RfrAndCommentRepository();
            TrackingRepository trackingRepo = new TrackingRepository();
            OdsRepository odsRepo = new OdsRepository();
            PageDataRepository pageDataRepo = new PageDataRepository();
            MetadatumRepository metadatumRepo = new MetadatumRepository();
            SocialMediaLinksRepository socialMediaLinksRepo = new SocialMediaLinksRepository();
            PreferencesRepository preferencesRepo = new PreferencesRepository();
            InstantMessagingRepository instantMessagingRepo = new InstantMessagingRepository();
            AdvertRepository advertRepo = new AdvertRepository();
            ImageUrlsRepository imageUrlsRepo = new ImageUrlsRepository();
            KeyFactsRepository keyFactsRepo = new KeyFactsRepository();
            VehicleRepository vehicleRepo = new VehicleRepository();
            SellerRepository sellerRepo = new SellerRepository();
            FinanceRepository financeRepo = new FinanceRepository();
            AutotraderResponseRepository autotraderResponseRepo = new AutotraderResponseRepository();

            foreach (Article article in articles)
            {
                MotResponse motResponse = new MotResponse();

                try
                {
                    if (UseSleep)
                    {
                        // Sleep for a bit before making next call to look human.
                        int sleep = new Random().Next(SleepMin, SleepMax);
                        Log.Info($"Sleeping for {sleep} ms.");
                        Thread.Sleep(sleep);
                    }

                    string link = RemoveNonNumeric.Replace(article.Link, String.Empty).Insert(0, AutoTraderApiEndPoint);

                    // Call Autotrader API.
                    string autotraderData = Proxy.MakeWebRequest(link, UseProxy, false);
                    if (String.IsNullOrWhiteSpace(autotraderData)) continue;

                    // Convert to response.
                    var autotraderResponse = JToken.Parse(autotraderData).ToObject<AutotraderResponse>();

                    // Call MOT API.
                    string url = $"{ApiHistoryUrl}{autotraderResponse.Vehicle.Vrm}";
                    string motDataString = Proxy.MakeApiRequest(url);
                    if (!String.IsNullOrWhiteSpace(motDataString))
                    {
                        var motData = JToken.Parse(motDataString);
                        motResponse = JToken.Parse(motData[0].ToString()).ToObject<MotResponse>();
                    }

                    // Save MOT response.
                    IList<MotTest> motTests = motResponse.VirtualMotTests?.ToList();
                    motResponse.VirtualMotTests = null;
                    motRepo.Create(motResponse);

                    foreach (MotTest motTest in motTests)
                    {
                        IList<RfrAndComment> rfrAndComments = motTest.VirtualRfrAndComments?.ToList();
                        motTest.VirtualRfrAndComments = null;
                        motTest.MotResponseId = motResponse.Id;
                        motTestRepo.Create(motTest);

                        foreach (RfrAndComment rfrAndComment in rfrAndComments)
                        {
                            rfrAndComment.MotTestId = motTest.Id;
                            rfrAndComment.Text = rfrAndComment.Text.Replace("  ", " "); // Clean up double spaces.
                            rfrAndComment.Text = rfrAndComment.Text.Replace("()", String.Empty); // Clean up empty brackets.
                            rfrAndCommentRepo.Create(rfrAndComment);
                        }
                    }

                    // Save Autotrader response.
                    Advert advert = autotraderResponse.Advert;
                    if (advert != null)
                    {
                        Preferences preferences = autotraderResponse.Advert.InstantMessaging?.Preferences;
                        if (preferences != null) preferencesRepo.Create(preferences);

                        InstantMessaging instantMessaging = autotraderResponse.Advert.InstantMessaging;
                        if (instantMessaging != null)
                        {
                            instantMessaging.Preferences = null;
                            instantMessaging.PreferencesId = preferences?.Id ?? null;
                            instantMessagingRepo.Create(instantMessaging);
                        }

                        SocialMediaLinks socialMediaLinks = autotraderResponse.Advert.SocialMediaLinks;
                        if (socialMediaLinks != null) socialMediaLinksRepo.Create(socialMediaLinks);

                        advert.ImageUrls = null; // Just to keep tidy even though this has no effect.
                        advert.InstantMessaging = null;
                        advert.SocialMediaLinks = null;
                        advert.VirtualImageUrls = null;
                        advert.InstantMessagingId = instantMessaging?.Id ?? null;
                        advert.SocialMediaLinksId = socialMediaLinks?.Id ?? null;
                        advertRepo.Create(advert);

                        if (autotraderResponse.Advert.ImageUrls != null)
                        {
                            foreach (string advertImageUrl in autotraderResponse.Advert.ImageUrls)
                            {
                                ImageUrls imageUrls = new ImageUrls
                                {
                                    AdvertId = advert?.Id ?? null,
                                    Url = advertImageUrl
                                };
                                imageUrlsRepo.Create(imageUrls);
                            }
                        }
                    }

                    Finance finance = autotraderResponse.Finance;
                    if (finance != null) financeRepo.Create(finance);

                    Vehicle vehicle = autotraderResponse.Vehicle;
                    if (vehicle != null)
                    {
                        KeyFacts keyFacts = autotraderResponse.Vehicle.KeyFacts;
                        if (keyFacts != null) keyFactsRepo.Create(keyFacts);

                        vehicle.KeyFacts = null;
                        vehicle.KeyFactsId = keyFacts?.Id ?? null;
                        vehicleRepo.Create(vehicle);
                    }

                    Seller seller = autotraderResponse.Seller;
                    if (seller != null)
                    {
                        if (!String.IsNullOrWhiteSpace(autotraderResponse.Seller.ProfileUrl))
                        {
                            autotraderResponse.Seller.ProfileUrl = autotraderResponse.Seller.ProfileUrl
                                .Insert(0, "https://www.autotrader.co.uk");
                        }

                        sellerRepo.Create(seller);
                    }

                    PageData pageData = autotraderResponse.PageData;
                    if (pageData != null)
                    {
                        IList<Metadatum> metadatums = autotraderResponse.PageData.VirtualMetadatas?.ToList();
                        Tracking tracking = autotraderResponse.PageData.Tracking;
                        Ods ods = autotraderResponse.PageData.Ods;

                        if (tracking != null) trackingRepo.Create(tracking);
                        if (ods != null) odsRepo.Create(ods);

                        pageData.VirtualMetadatas = null;
                        pageData.Tracking = null;
                        pageData.Ods = null;
                        pageData.TrackingId = tracking?.Id ?? null;
                        pageData.OdsId = ods?.Id ?? null;
                        pageDataRepo.Create(pageData);

                        if (metadatums != null)
                        {
                            foreach (Metadatum metadatum in metadatums)
                            {
                                metadatum.PageData = null;
                                metadatum.PageDataId = pageData.Id;
                                metadatumRepo.Create(metadatum);
                            }
                        }
                    }

                    autotraderResponse.Advert = null;
                    autotraderResponse.Finance = null;
                    autotraderResponse.Vehicle = null;
                    autotraderResponse.Seller = null;
                    autotraderResponse.PageData = null;
                    autotraderResponse.AdvertId = advert?.Id ?? null;
                    autotraderResponse.FinanceId = finance?.Id ?? null;
                    autotraderResponse.VehicleId = vehicle?.Id ?? null;
                    autotraderResponse.SellerId = seller?.Id ?? null;
                    autotraderResponse.PageDataId = pageData?.Id ?? null;
                    autotraderResponseRepo.Create(autotraderResponse);

                    // Add to parent object.
                    var apiArticle = new ApiArticle
                    {
                        AutotraderResponseId = autotraderResponse.Id,
                        MotResponseId = motResponse?.Id ?? null,
                        ArticleId = article.Id
                    };

                    ApiArticleRepo.Create(apiArticle);
                    msg = "with API article";
                }
                catch (Exception ex)
                {
                    Log.Warn(ex);
                    msg = "without API article";
                }
            }
            return msg;
        }
    }
}
