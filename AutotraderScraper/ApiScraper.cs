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
        private static readonly Regex ImageResizeFind;
        private static readonly string ImageResizeReplace;
        private static readonly ApiArticleVersionRepository ApiArticleVersionRepo;
        private static readonly MotResponseRepository MotRepo;
        private static readonly MotTestRepository MotTestRepo;
        private static readonly RfrAndCommentRepository RfrAndCommentRepo;
        private static readonly TrackingRepository TrackingRepo;
        private static readonly OdsRepository OdsRepo;
        private static readonly PageDataRepository PageDataRepo;
        private static readonly MetadatumRepository MetadatumRepo;
        private static readonly SocialMediaLinksRepository SocialMediaLinksRepo;
        private static readonly PreferencesRepository PreferencesRepo;
        private static readonly InstantMessagingRepository InstantMessagingRepo;
        private static readonly AdvertRepository AdvertRepo;
        private static readonly ImageUrlsRepository ImageUrlsRepo;
        private static readonly KeyFactsRepository KeyFactsRepo;
        private static readonly VehicleRepository VehicleRepo;
        private static readonly SellerRepository SellerRepo;
        private static readonly FinanceRepository FinanceRepo;
        private static readonly AutotraderResponseRepository AutotraderResponseRepo;

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
            ImageResizeFind = new Regex(ConfigurationManager.AppSettings["AutotraderImageResizeRegexFind"]);
            ImageResizeReplace = ConfigurationManager.AppSettings["AutotraderImageResizeRegexReplace"];
            ApiArticleVersionRepo = new ApiArticleVersionRepository();
            MotRepo = new MotResponseRepository();
            MotTestRepo = new MotTestRepository();
            RfrAndCommentRepo = new RfrAndCommentRepository();
            TrackingRepo = new TrackingRepository();
            OdsRepo = new OdsRepository();
            PageDataRepo = new PageDataRepository();
            MetadatumRepo = new MetadatumRepository();
            SocialMediaLinksRepo = new SocialMediaLinksRepository();
            PreferencesRepo = new PreferencesRepository();
            InstantMessagingRepo = new InstantMessagingRepository();
            AdvertRepo = new AdvertRepository();
            ImageUrlsRepo = new ImageUrlsRepository();
            KeyFactsRepo = new KeyFactsRepository();
            VehicleRepo = new VehicleRepository();
            SellerRepo = new SellerRepository();
            FinanceRepo = new FinanceRepository();
            AutotraderResponseRepo = new AutotraderResponseRepository();
        }

        public static void Run(ArticleVersion articleVersion, string link)
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

                link = RemoveNonNumeric.Replace(link, String.Empty).Insert(0, AutoTraderApiEndPoint);

                // Call Autotrader API.
                string autotraderData = Proxy.MakeWebRequest(link, UseProxy, false);
                if (String.IsNullOrWhiteSpace(autotraderData)) return;

                // Convert to response.
                var autotraderResponse = JToken.Parse(autotraderData).ToObject<AutotraderResponse>();

                // Call MOT API.
                if (autotraderResponse?.Vehicle?.Vrm != null)
                {
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
                    MotRepo.Create(motResponse);

                    if (motTests != null)
                    {
                        foreach (MotTest motTest in motTests)
                        {
                            IList<RfrAndComment> rfrAndComments = motTest.VirtualRfrAndComments?.ToList();
                            motTest.VirtualRfrAndComments = null;
                            motTest.MotResponseId = motResponse.Id;
                            MotTestRepo.Create(motTest);

                            if (rfrAndComments == null) continue;
                            foreach (RfrAndComment rfrAndComment in rfrAndComments)
                            {
                                rfrAndComment.MotTestId = motTest.Id;
                                rfrAndComment.Text = rfrAndComment.Text.Replace("  ", " "); // Clean up double spaces.
                                rfrAndComment.Text = rfrAndComment.Text.Replace("()", String.Empty); // Clean up empty brackets.
                                RfrAndCommentRepo.Create(rfrAndComment);
                            }
                        }
                    }
                }

                // Save Autotrader response.
                Advert advert = autotraderResponse?.Advert;
                if (advert != null)
                {
                    Preferences preferences = autotraderResponse.Advert.InstantMessaging?.Preferences;
                    if (preferences != null) PreferencesRepo.Create(preferences);

                    InstantMessaging instantMessaging = autotraderResponse.Advert.InstantMessaging;
                    if (instantMessaging != null)
                    {
                        instantMessaging.Preferences = null;
                        instantMessaging.PreferencesId = preferences?.Id;
                        InstantMessagingRepo.Create(instantMessaging);
                    }

                    SocialMediaLinks socialMediaLinks = autotraderResponse.Advert.SocialMediaLinks;
                    if (socialMediaLinks != null) SocialMediaLinksRepo.Create(socialMediaLinks);

                    advert.InstantMessaging = null;
                    advert.SocialMediaLinks = null;
                    advert.VirtualImageUrls = null;
                    advert.SocialMediaLinksId = socialMediaLinks?.Id;
                    advert.InstantMessagingId = instantMessaging?.Id;
                    if (advert.MainImageUrl != null) advert.MainImageUrl = ImageResizeFind.Replace(advert.MainImageUrl, ImageResizeReplace);
                    AdvertRepo.Create(advert);

                    if (autotraderResponse.Advert.ImageUrls?.Count > 0)
                    {
                        foreach (string advertImageUrl in autotraderResponse.Advert.ImageUrls)
                        {
                            string imageUrl = ImageResizeFind.Replace(advertImageUrl, ImageResizeReplace);
                            ImageUrlsRepo.Create(new ImageUrls
                            {
                                AdvertId = advert.Id,
                                Url = imageUrl
                            });
                        }
                    }
                }

                Finance finance = autotraderResponse?.Finance;
                if (finance != null) FinanceRepo.Create(finance);

                Vehicle vehicle = autotraderResponse.Vehicle;
                if (vehicle != null)
                {
                    KeyFacts keyFacts = autotraderResponse.Vehicle.KeyFacts;
                    if (keyFacts != null) KeyFactsRepo.Create(keyFacts);

                    vehicle.KeyFacts = null;
                    vehicle.KeyFactsId = keyFacts?.Id ?? null;
                    VehicleRepo.Create(vehicle);
                }

                Seller seller = autotraderResponse.Seller;
                if (seller != null)
                {
                    if (!String.IsNullOrWhiteSpace(autotraderResponse.Seller.ProfileUrl))
                    {
                        autotraderResponse.Seller.ProfileUrl = autotraderResponse.Seller.ProfileUrl
                            .Insert(0, "https://www.autotrader.co.uk");
                    }
                    if (seller.BannerUrl != null && seller.BannerUrl.Contains("images/null")) seller.BannerUrl = null;
                    SellerRepo.Create(seller);
                }

                PageData pageData = autotraderResponse.PageData;
                if (pageData != null)
                {
                    IList<Metadatum> metadatums = autotraderResponse.PageData.VirtualMetadatas?.ToList();
                    Tracking tracking = autotraderResponse.PageData.Tracking;
                    Ods ods = autotraderResponse.PageData.Ods;

                    if (tracking != null) TrackingRepo.Create(tracking);
                    if (ods != null) OdsRepo.Create(ods);

                    pageData.VirtualMetadatas = null;
                    pageData.Tracking = null;
                    pageData.Ods = null;
                    pageData.TrackingId = tracking?.Id ?? null;
                    pageData.OdsId = ods?.Id ?? null;
                    PageDataRepo.Create(pageData);

                    if (metadatums != null)
                    {
                        foreach (Metadatum metadatum in metadatums)
                        {
                            metadatum.PageData = null;
                            metadatum.PageDataId = pageData.Id;
                            MetadatumRepo.Create(metadatum);
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
                AutotraderResponseRepo.Create(autotraderResponse);

                // Add to parent object.
                ApiArticleVersionRepo.Create(new ApiArticleVersion
                {
                    ArticleVersionId = articleVersion.Id,
                    AutotraderResponseId = autotraderResponse.Id,
                    MotResponseId = motResponse?.Id ?? null
                });
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("404")) throw;
                Log.Warn("Could not save API article due to error that is not a 404.", ex);
            }
        }
    }
}
