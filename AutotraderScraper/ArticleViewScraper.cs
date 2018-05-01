using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading;
using AutotraderScraper.Model;
using AutotraderScraper.Repository;
using HtmlAgilityPack;
using log4net;

namespace AutotraderScraper
{
    public class ArticleViewScraper
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Proxy _proxy = Proxy.Instance;
        private readonly ArticleRepository _articleRepo = new ArticleRepository();
        private readonly bool _useSleep = bool.Parse(ConfigurationManager.AppSettings["UseSleep"]);
        private readonly int _sleepMin = int.Parse(ConfigurationManager.AppSettings["MinSleepMilliSecs"]);
        private readonly int _sleepMax = int.Parse(ConfigurationManager.AppSettings["MaxSleepMilliSecs"]);

        public ArticleViewScraper(Stack<string> links)
        {
            try
            {
                // Scrape article view stack.
                while (links.Count > 0)
                {
                    string link = links.Pop();

                    try
                    {
                        if (_useSleep)
                        {
                            // Sleep for a bit before making next call to look human.
                            int sleep = new Random().Next(_sleepMin, _sleepMax);
                            _log.Info($"Sleeping for {sleep} ms.");
                            Thread.Sleep(sleep);
                        }

                        _log.Info($"Scraping view: {link}");

                        // Web request response will be read into this variable.
                        string data;

                        try
                        {
                            data = _proxy.MakeRequest(link);
                        }
                        catch (Exception ex)
                        {
                            _log.Error("Could not get web response for article view.", ex.GetBaseException());
                            continue;
                        }

                        // If no response, skip to next.
                        if (String.IsNullOrEmpty(data))
                        {
                            _log.Error("Skipping view page due to null content.");
                            continue;
                        }

                        // Parse response as HTML document.
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(data);

                        // If title doesn't exist, then the article has expired.
                        try
                        {
                            new string(doc.DocumentNode.SelectSingleNode(@"//*[@id=""main-content""]/div[1]/h1/span[1]").InnerText.ToCharArray());
                        }
                        catch (Exception)
                        {
                            _log.Info("Setting article as inactive.");
                            Article inactiveArticle = _articleRepo.Get(x => x.Link == link);
                            inactiveArticle.Active = false;
                            _articleRepo.Update(inactiveArticle);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"Could not scrape link: {link}", ex.GetBaseException());
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("Could not scrape in Article View Mode.", ex.GetBaseException());
            }
            finally
            {
                _log.Info("Article View Scraper finished scraping.");
            }
        }
    }
}
