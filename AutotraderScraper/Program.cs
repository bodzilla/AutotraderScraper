using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using AutotraderScraper.Repository;
using log4net;

namespace AutotraderScraper
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static bool _failed;

        public static readonly Stack<string> ArticleViewStack = new Stack<string>();

        private static void Main(string[] args)
        {
            try
            {
                // Update days old for all existing articles and deletes non-existing articles.
                if (args.Length > 0)
                {
                    Log.Info("Starting Back Burner Mode..");

                    // Only get active articles.
                    foreach (string link in new ArticleRepository().GetList(x => x.Active).OrderByDescending(x => x.DateAdded).Select(x => x.Link)) ArticleViewStack.Push(link);
                    new ArticleViewScraper().Run(ArticleViewStack);
                }
                else
                {
                    Log.Info("Retrieving runtime variables..");

                    // Get scrape lists with respective page counts.
                    string[][] scrapeListArray = ConfigurationManager.AppSettings.AllKeys
                        .Where(key => key.StartsWith("Scrape"))
                        .Select(key => ConfigurationManager.AppSettings[key].Split(' '))
                        .ToArray();

                    // Run all search lists.
                    Log.Info("Starting Search List Scraper..");
                    foreach (string[] list in scrapeListArray) new SearchListScraper().Run(int.Parse(list[0]), list[1]);
                }
            }
            catch (Exception ex)
            {
                _failed = true;
                Log.Fatal("Could not run AutotraderScraper.", ex);
            }
            finally
            {
                Log.Info("Scraping complete. Exiting AutotraderScraper..");
                Thread.Sleep(10000);
                if (_failed) Environment.Exit(1);
                Environment.Exit(0);
            }
        }
    }
}
