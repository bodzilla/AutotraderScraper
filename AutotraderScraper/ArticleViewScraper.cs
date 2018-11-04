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
    internal class ArticleViewScraper
    {
        private readonly ILog _log;
        private readonly ArticleRepository _articleRepo;
        private readonly bool _useSleep;
        private readonly int _sleepMin;
        private readonly int _sleepMax;

        public ArticleViewScraper()
        {
            // Initialise objects.
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _articleRepo = new ArticleRepository();

            // Load settings.
            _useSleep = bool.Parse(ConfigurationManager.AppSettings["UseSleep"]);
            _sleepMin = int.Parse(ConfigurationManager.AppSettings["MinSleepMilliSecs"]);
            _sleepMax = int.Parse(ConfigurationManager.AppSettings["MaxSleepMilliSecs"]);
        }

        public void Run(Stack<string> links)
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
                            data = Proxy.MakeRequest(link);
                        }
                        catch (Exception ex)
                        {
                            _log.Error("Could not get web response for article view.", ex);
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
                        _log.Error($"Could not scrape link: {link}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("Fatal exception(s) occured in Article View Scraper.", ex);
            }
            finally
            {
                _log.Info("Article View Scraper finished scraping.");
            }
        }
    }
}
