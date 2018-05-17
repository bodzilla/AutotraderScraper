using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using AutotraderScraper.Model;
using AutotraderScraper.Repository;
using HtmlAgilityPack;
using log4net;

namespace AutotraderScraper
{
    internal class SearchListScraper
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Proxy _proxy = Proxy.Instance;
        private readonly CarMakeRepository _carMakeRepo = new CarMakeRepository();
        private readonly CarModelRepository _carModelRepo = new CarModelRepository();
        private readonly ArticleRepository _articleRepo = new ArticleRepository();
        private readonly ArticleVersionRepository _articleVersionRepo = new ArticleVersionRepository();

        private static readonly int PageNumberRetries = int.Parse(ConfigurationManager.AppSettings["PageNumberRetries"]);
        private readonly bool _useSleep = bool.Parse(ConfigurationManager.AppSettings["UseSleep"]);
        private readonly IList<string> _bodyTypesList = ConfigurationManager.AppSettings.AllKeys.Where(key => key.Contains("BodyType")).Select(key => ConfigurationManager.AppSettings[key]).ToList();
        private readonly IList<string> _fuelTypesList = ConfigurationManager.AppSettings.AllKeys.Where(key => key.Contains("FuelType")).Select(key => ConfigurationManager.AppSettings[key]).ToList();
        private readonly IList<string> _transmissionTypesList = ConfigurationManager.AppSettings.AllKeys.Where(key => key.Contains("TransmissionType"))
            .Select(key => ConfigurationManager.AppSettings[key]).ToList();
        private readonly int _sleepMin = int.Parse(ConfigurationManager.AppSettings["MinSleepMilliSecs"]);
        private readonly int _sleepMax = int.Parse(ConfigurationManager.AppSettings["MaxSleepMilliSecs"]);
        private readonly string _noImageLink = ConfigurationManager.AppSettings["NoImageLink"];

        private readonly HashSet<Article> _articleList = new HashSet<Article>();
        private readonly HashSet<string> _articleLinksList = new HashSet<string>();

        private readonly Regex _removeNonNumeric = new Regex(@"[^\d]");
        private readonly Regex _matchLs = new Regex(@".*L\b");
        private readonly Regex _removeLs = new Regex("L");

        private readonly int _failedArticles;

        public SearchListScraper(string l)
        {
            string carMake = String.Empty;
            string carModel = String.Empty;

            try
            {
                // Setting initial variables.
                int? pages = null; // Set this later.
                string url = l;
                carMake = ToTitleCase(HttpUtility.ParseQueryString(url).Get("make"));
                carModel = ToTitleCase(HttpUtility.ParseQueryString(url).Get("model"));

                // Check if car make and model exist in db.
                _log.Info($"Asserting existence of {carMake} {carModel} in database..");
                bool carMakeExists = _carMakeRepo.Exists(x => x.Name.Equals(carMake, StringComparison.CurrentCultureIgnoreCase));
                bool carModelExists = _carModelRepo.Exists(x => x.Name.Equals(carModel, StringComparison.CurrentCultureIgnoreCase));

                // Create/get and set ids.
                if (!carMakeExists) _carMakeRepo.Create(new CarMake { Name = carMake });
                if (!carModelExists) _carModelRepo.Create(new CarModel { Name = carModel, CarMakeId = _carMakeRepo.Get(x => x.Name.Equals(carMake)).Id });
                int carModelId = _carModelRepo.Get(x => x.Name.Equals(carModel)).Id;

                // Get all articles and article links.
                _log.Info("Retrieving indexes..");
                _articleList.UnionWith(_articleRepo.GetAll().Where(x => x.CarModelId == carModelId && x.Active));
                _articleLinksList.UnionWith(_articleRepo.GetAll().Where(x => x.CarModelId == carModelId && x.Active).Select(x => x.Link));

                // First get total number of pages.
                try
                {
                    _log.Info("Retrieving page count from web..");

                    // Do retries on page count as this may not appear on some calls.
                    int retryCount = 0;
                    while (retryCount <= PageNumberRetries)
                    {
                        // Web request response will be read into this variable.
                        string data = _proxy.MakeRequest(url);

                        // Parse response as HTML document.
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(data);
                        string pagesNode;

                        try
                        {
                            pagesNode = doc.DocumentNode.SelectSingleNode(@"//*[@id=""main-content""]/div[1]/header/nav/ul/li[3]/strong[2]").InnerText.Trim();
                        }
                        catch (Exception)
                        {
                            if (retryCount == PageNumberRetries) throw new Exception("Pages node returns null.");
                            retryCount++;
                            continue;
                        }

                        pages = int.Parse(pagesNode);
                        _log.Info($"{pages} pages found.");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _log.Fatal("Could not get page count from web.", ex.GetBaseException());
                    throw;
                }

                // Scrape search list by paging through pageset.
                for (int i = 1; i <= pages; i++)
                {
                    try
                    {
                        if (_useSleep)
                        {
                            // Sleep for a bit before making next call to look human.
                            int sleep = new Random().Next(_sleepMin, _sleepMax);
                            _log.Info($"Sleeping for {sleep} ms.");
                            Thread.Sleep(sleep);
                        }

                        // Set page.
                        string currentPage = $"{url}&page={i}";
                        _log.Info($"Scraping page: {currentPage}");

                        // Web request response will be read into this variable.
                        string data;

                        try
                        {
                            data = _proxy.MakeRequest(currentPage);
                        }
                        catch (Exception ex)
                        {
                            _log.Error($"Could not get web response for url: {currentPage}", ex.GetBaseException());
                            continue;
                        }

                        // If no response, skip to next.
                        if (String.IsNullOrEmpty(data))
                        {
                            _log.Error("Skipping scrape page due to null content.");
                            continue;
                        }

                        // Parse response as HTML document.
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(data);
                        HtmlNodeCollection results = doc.DocumentNode.SelectNodes(@"//*[@id=""main-content""]/div[1]/ul/li[""search-page__result""]/article");

                        foreach (HtmlNode result in results)
                        {
                            string path = result.XPath;

                            try
                            {
                                string price = result.SelectSingleNode($"{path}/section[2]/a/div").InnerText.Trim();
                                if (String.IsNullOrEmpty(price)) continue; // If price doesn't exist, this article has expired.

                                // Get article values, split at '?' to remove excess trailing from url.
                                string link = result.SelectSingleNode($"{path}/section[1]/div/h2/a").GetAttributeValue("href", null).Split('?')[0];

                                // Add domain name to link value.
                                link = link.Insert(0, "https://www.autotrader.co.uk");

                                string location;
                                try { location = result.SelectSingleNode($"{path}/section[1]/div/div/div[2]/span").InnerText.Trim(); }
                                catch (Exception) { location = ConfigurationManager.AppSettings["DefaultLocation"]; }

                                string priceTag;
                                try { priceTag = result.SelectSingleNode($"{path}/section[2]/div[1]/span[1]").InnerText.Trim(); }
                                catch (Exception) { priceTag = null; }

                                string thumbnail;
                                string title;
                                string teaser;
                                string description;
                                string year;
                                string sellerType;
                                string bodyType = null;
                                string mileage = null;
                                string transmissionType = null;
                                string engineSize = null;
                                string bhp = null;
                                string fuelType = null;
                                string updates = null;

                                try
                                {
                                    thumbnail = result.SelectSingleNode($"{path}/section[1]/figure/a/img").GetAttributeValue("src", null).Trim();
                                    title = result.SelectSingleNode($"{path}/section[1]/div/h2/a").InnerText.Trim();
                                    teaser = String.IsNullOrEmpty(result.SelectSingleNode($"{path}/section[1]/div/p[1]").InnerText.Trim())
                                        ? null : result.SelectSingleNode($"{path}/section[1]/div/p[1]").InnerText.Trim();
                                    description = result.SelectSingleNode($"{path}/section[1]/div/p[2]").InnerText.Trim();
                                    sellerType = result.SelectSingleNode($"{path}/section[1]/div/div/div[1]").InnerText.Trim();
                                    year = result.SelectSingleNode($"{path}/section[1]/div/ul[1]/li[1]").InnerText.Substring(0, 4).Trim();

                                    IList<string> attributes = new List<string>();
                                    int attributeCount = result.SelectNodes($"{path}/section[1]/div/ul[1]/li").Count;

                                    // First [li] object is the year, so start from second.
                                    for (int j = 2; j <= attributeCount; j++) attributes.Add(result.SelectSingleNode($"{path}/section[1]/div/ul[1]/li[{j}]").InnerText.Trim());

                                    foreach (string attribute in attributes)
                                    {
                                        // Sometimes the year is missing from original field as it's within attributes.
                                        if (String.IsNullOrEmpty(year))
                                        {
                                            try
                                            {
                                                int number = int.Parse(_removeNonNumeric.Replace(attribute.Substring(0, 4), String.Empty));
                                                year = number.ToString();
                                                continue;
                                            }
                                            catch (Exception)
                                            {
                                                year = null;
                                            }
                                        }

                                        if (attribute.Contains("bhp"))
                                        {
                                            bhp = attribute;
                                            continue;
                                        }

                                        if (attribute.Contains("mile"))
                                        {
                                            mileage = attribute;
                                            continue;
                                        }

                                        if (_matchLs.IsMatch(attribute))
                                        {
                                            engineSize = attribute;
                                            continue;
                                        }

                                        if (_bodyTypesList.Any(x => x.Equals(attribute)))
                                        {
                                            bodyType = attribute;
                                            continue;
                                        }

                                        if (_fuelTypesList.Any(x => x.Equals(attribute)))
                                        {
                                            fuelType = attribute;
                                            continue;
                                        }

                                        if (_transmissionTypesList.Any(x => x.Equals(attribute)))
                                        {
                                            transmissionType = attribute;
                                            continue;
                                        }
                                        throw new Exception($"No attribute matches found for value: {attribute}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _log.Error("Could not scrape field(s).", ex.GetBaseException());
                                    continue;
                                }

                                // Cleanse results.
                                title = WebUtility.HtmlDecode(title);
                                teaser = WebUtility.HtmlDecode(teaser);
                                description = WebUtility.HtmlDecode(description);
                                if (thumbnail.Equals(_noImageLink)) thumbnail = null;
                                location = ToTitleCase(location);
                                year = _removeNonNumeric.Replace(year, String.Empty);
                                if (mileage != null) mileage = _removeNonNumeric.Replace(mileage, String.Empty);
                                engineSize = _removeLs.Replace(engineSize, String.Empty);
                                if (Math.Abs(double.Parse(engineSize) % 1) <= Double.Epsilon * 100) engineSize = $"{Math.Round(double.Parse(engineSize))}";
                                if (bhp != null) bhp = _removeNonNumeric.Replace(bhp, String.Empty);
                                sellerType = sellerType.Contains("Trade") ? "Trade" : "Private";
                                if (!String.IsNullOrEmpty(priceTag)) priceTag = ToTitleCase(priceTag);
                                price = _removeNonNumeric.Replace(price, String.Empty);

                                // De-duplication.
                                // First, check if article link exists in db.
                                ArticleVersion dbArticleVersion = null;
                                Article dbArticle = null;
                                bool articleLinkExists = _articleLinksList.Contains(link);

                                if (articleLinkExists)
                                {
                                    try
                                    {
                                        // Set existing article and latest article version.
                                        dbArticle = _articleList.Single(x => x.Link == link);
                                        dbArticleVersion = dbArticle.VirtualArticleVersions.OrderByDescending(x => x.Version).First();
                                    }
                                    catch (Exception)
                                    {
                                        _log.Error("Could not get dbArticle/dbArticleVersion, removing from db.");
                                        if (dbArticleVersion != null) { _articleVersionRepo.Delete(dbArticleVersion); }
                                        if (dbArticle != null) _articleRepo.Delete(dbArticle);
                                        if (_articleList.Contains(dbArticle)) _articleList.Remove(dbArticle);
                                        if (_articleLinksList.Contains(link)) _articleLinksList.Remove(link);
                                        continue;
                                    }

                                    // Hash db article version.
                                    byte[] dbTitleBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Title);
                                    byte[] dbLocationBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Location);
                                    byte[] dbTeaserBytes = { };
                                    if (dbArticleVersion.Teaser != null) dbTeaserBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Teaser);
                                    byte[] dbDescriptionBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Description);
                                    byte[] dbYearBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Year.ToString());
                                    byte[] dbBodyTypeBytes = { };
                                    if (dbArticleVersion.BodyType != null) dbBodyTypeBytes = Encoding.ASCII.GetBytes(dbArticleVersion.BodyType);
                                    byte[] dbMileageBytes = { };
                                    if (dbArticleVersion.Mileage != null) dbMileageBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Mileage.ToString());
                                    byte[] dbTransmissionTypeBytes = { };
                                    if (dbArticleVersion.TransmissionType != null) dbTransmissionTypeBytes = Encoding.ASCII.GetBytes(dbArticleVersion.TransmissionType);
                                    byte[] dbEngineSizeBytes = Encoding.ASCII.GetBytes(dbArticleVersion.EngineSize.ToString(CultureInfo.CurrentCulture));
                                    byte[] dbFuelTypeBytes = { };
                                    if (dbArticleVersion.FuelType != null) dbFuelTypeBytes = Encoding.ASCII.GetBytes(dbArticleVersion.FuelType);
                                    byte[] dbSellerTypeBytes = Encoding.ASCII.GetBytes(dbArticleVersion.SellerType);
                                    byte[] dbPriceBytes = Encoding.ASCII.GetBytes(dbArticleVersion.Price.ToString());
                                    byte[] dbBytes = CombineBytes(dbTitleBytes, dbLocationBytes, dbTeaserBytes, dbDescriptionBytes, dbYearBytes, dbBodyTypeBytes, dbMileageBytes,
                                        dbTransmissionTypeBytes, dbEngineSizeBytes, dbFuelTypeBytes, dbSellerTypeBytes, dbPriceBytes);
                                    string dbHash = GenerateHash(dbBytes);

                                    // Hash fetched verison of this article.
                                    byte[] titleBytes = Encoding.ASCII.GetBytes(title);
                                    byte[] locationBytes = Encoding.ASCII.GetBytes(location);
                                    byte[] teaserBytes = { };
                                    if (teaser != null) teaserBytes = Encoding.ASCII.GetBytes(teaser);
                                    byte[] descriptionBytes = Encoding.ASCII.GetBytes(description);
                                    byte[] yearBytes = Encoding.ASCII.GetBytes(year);
                                    byte[] bodyTypeBytes = { };
                                    if (bodyType != null) bodyTypeBytes = Encoding.ASCII.GetBytes(bodyType);
                                    byte[] mileageBytes = { };
                                    if (mileage != null) mileageBytes = Encoding.ASCII.GetBytes(mileage);
                                    byte[] transmissionTypeBytes = { };
                                    if (transmissionType != null) transmissionTypeBytes = Encoding.ASCII.GetBytes(transmissionType);
                                    byte[] engineSizeBytes = Encoding.ASCII.GetBytes(engineSize);
                                    byte[] fuelTypeBytes = { };
                                    if (fuelType != null) fuelTypeBytes = Encoding.ASCII.GetBytes(fuelType);
                                    byte[] sellerTypeBytes = Encoding.ASCII.GetBytes(sellerType);
                                    byte[] priceBytes = Encoding.ASCII.GetBytes(price);
                                    byte[] bytes = CombineBytes(titleBytes, locationBytes, teaserBytes, descriptionBytes, yearBytes, bodyTypeBytes, mileageBytes,
                                        transmissionTypeBytes, engineSizeBytes, fuelTypeBytes, sellerTypeBytes, priceBytes);
                                    string hash = GenerateHash(bytes);

                                    // Compare hashes, skip saving if they are the same as this means we have the latest version.
                                    if (String.Equals(dbHash, hash))
                                    {
                                        bool updateArticle = false;
                                        bool updateArticleVersion = false;

                                        // Although we have the latest article version, we may like to update some fields.
                                        try
                                        {
                                            // Update article version BHP.
                                            if (!String.IsNullOrEmpty(bhp) && !String.Equals(dbArticleVersion.Bhp.ToString(), bhp))
                                            {
                                                updateArticleVersion = true;
                                                dbArticleVersion.Bhp = int.Parse(bhp);
                                            }

                                            // Update article thumbnail.
                                            if (thumbnail != null && !String.Equals(dbArticle.Thumbnail, thumbnail))
                                            {
                                                updateArticle = true;
                                                dbArticle.Thumbnail = thumbnail;
                                            }

                                            // Update article price tag.
                                            if (!String.Equals(dbArticle.PriceTag, priceTag))
                                            {
                                                updateArticle = true;
                                                dbArticle.PriceTag = priceTag;
                                            }

                                            if (updateArticleVersion) _articleVersionRepo.Update(dbArticleVersion);
                                            if (updateArticle) _articleRepo.Update(dbArticle);
                                        }
                                        catch (Exception ex)
                                        {
                                            _log.Error("Could not update existing article/article version.", ex.GetBaseException());
                                        }

                                        _log.Info("Skipped duplicate article.");
                                        continue;
                                    }

                                    // Check if price changed.
                                    if (int.Parse(price) > dbArticleVersion.Price) updates += $"Price increased from £{dbArticleVersion.Price:N0}. ";
                                    if (int.Parse(price) < dbArticleVersion.Price) updates += $"Price decreased from £{dbArticleVersion.Price:N0}. ";

                                    // Check if mileage changed.
                                    if (int.Parse(mileage) > dbArticleVersion.Mileage) updates += $"Mileage increased from {dbArticleVersion.Mileage:N0}. ";
                                    if (int.Parse(mileage) < dbArticleVersion.Mileage) updates += $"Mileage decreased from {dbArticleVersion.Mileage:N0}. ";
                                }

                                // Init vars for db save.
                                Article article = new Article();
                                ArticleVersion articleVersion = new ArticleVersion();
                                string articleState;

                                if (dbArticle == null)
                                {
                                    // New article.
                                    articleState = "new";
                                    article.Link = link;
                                    article.PriceTag = priceTag;
                                    article.Thumbnail = thumbnail;
                                    article.CarModelId = carModelId;
                                    _articleRepo.Create(article);
                                    articleVersion.ArticleId = article.Id; // Link new article to article version.
                                    articleVersion.Version = 1; // Set first version.
                                }
                                else
                                {
                                    // Existing article.
                                    articleState = "existing";
                                    articleVersion.ArticleId = dbArticle.Id; // Link existing article.
                                    articleVersion.Version = dbArticleVersion.Version + 1; // Increment version.
                                }

                                // Set values and save.
                                articleVersion.Title = title;
                                articleVersion.Location = location;
                                articleVersion.Teaser = teaser;
                                articleVersion.Description = description;
                                articleVersion.Year = int.Parse(year);
                                articleVersion.BodyType = bodyType;
                                articleVersion.Mileage = mileage != null ? int.Parse(mileage) : (int?)null;
                                articleVersion.TransmissionType = transmissionType;
                                articleVersion.EngineSize = double.Parse(engineSize);
                                articleVersion.Bhp = bhp != null ? int.Parse(bhp) : (int?)null;
                                articleVersion.FuelType = fuelType;
                                articleVersion.SellerType = sellerType;
                                articleVersion.Price = int.Parse(price);
                                articleVersion.Updates = updates;
                                _articleVersionRepo.Create(articleVersion);

                                // Add to hash sets.
                                if (dbArticle == null)
                                {
                                    article.VirtualArticleVersions.Add(articleVersion);
                                    _articleList.Add(article);
                                    _articleLinksList.Add(link);
                                }
                                else
                                {
                                    // Remove article from list and add back with newly attached article version.
                                    _articleList.Remove(dbArticle);
                                    dbArticle.VirtualArticleVersions.Add(articleVersion);
                                    _articleList.Add(dbArticle);
                                }

                                _log.Info($"Saved new article version with {articleState} article.");
                            }
                            catch (Exception ex)
                            {
                                _log.Error("Could not process and save article/article version.", ex.GetBaseException());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _failedArticles++;
                        _log.Error($"Could not get or process scrape for page {pages}.", ex.GetBaseException());
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("Fatal exception(s) occured in Search List Scraper.", ex.GetBaseException());
            }
            finally
            {
                if (_failedArticles > 0) _log.Info($"{_failedArticles} articles failed.");
                _log.Info($"Search List Scraper finished scraping for {carMake} {carModel}.");
            }
        }

        private static string GenerateHash(byte[] data)
        {
            StringBuilder hash = new StringBuilder();

            // Use input string to calculate MD5 hash.
            MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(data);

            // Convert the byte array to hexadecimal string.
            foreach (byte _byte in hashBytes) hash.Append(_byte.ToString("X2"));
            return $"0x{hash}";
        }

        private static byte[] CombineBytes(params byte[][] arrays)
        {
            // Combine multiple byte arrays.
            byte[] combinedBytes = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, combinedBytes, offset, array.Length);
                offset += array.Length;
            }
            return combinedBytes;
        }

        private static string ToTitleCase(string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
    }
}
