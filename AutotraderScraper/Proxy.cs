using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;

namespace AutotraderScraper
{
    internal static class Proxy
    {
        private static readonly ILog Log;
        private static readonly Stack<string> ProxyList;
        private static readonly IList<string> UserAgents;
        private static string _ip;
        private static int? _port;

        private static readonly string ApiKey;

        static Proxy()
        {
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var useProxyScraper = bool.Parse(ConfigurationManager.AppSettings["UseProxyScraper"]);
            ApiKey = ConfigurationManager.AppSettings["ApiMotKey"];
            UserAgents = ConfigurationManager.AppSettings.AllKeys.Where(key => key.Contains("UserAgent")).Select(key => ConfigurationManager.AppSettings[key]).ToList();
            ProxyList = new Stack<string>();

            if (!useProxyScraper) return;

            // Get IP for proxy to use for calls.
            Log.Info("Retrieving proxy list from web..");

            // Get proxy IP from server.
            string proxyUrl = ConfigurationManager.AppSettings["ProxyUrl"];
            int timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(proxyUrl);
            request.UserAgent = SetUserAgent(true);
            request.Timeout = timeout;

            // Web request response will be read into this variable.
            string data;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException("Proxy list empty.")))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal("Could not get proxy details.", ex);
                throw;
            }

            IList<string> proxies = new List<string>();
            MatchCollection regMatch = Regex.Matches(data, @"([0-9]+(?:\.[0-9]+){3}:[0-9]+)");
            if (regMatch.Count > 0)
            {
                IList<string> matches = regMatch.OfType<Match>().Select(x => x.Value).ToList();
                foreach (string match in matches) proxies.Add(match);
            }
            else throw new Exception("Proxy list is empty.");

            // Shuffle the proxy list.
            ProxyList = new Stack<string>(Shuffle(proxies));
        }

        public static string MakeWebRequest(string url, bool useProxy, bool isWebScraper)
        {
            string data = String.Empty;
            if (useProxy)
            {
                if (ProxyList.Count < 1) throw new Exception("Proxy list has been exhuasted with no successes.");

                // Keep trying different proxies till success.
                while (String.IsNullOrEmpty(data) && ProxyList.Count > 0)
                {
                    try
                    {
                        data = DownloadString(url, true, isWebScraper);
                        if (String.IsNullOrWhiteSpace(data) || data.Contains("Please update your Google Chrome for the best experience")) Next();
                    }
                    catch (Exception)
                    {
                        _ip = null;
                        _port = null;
                    }
                }
            }
            else
            {
                data = DownloadString(url, false, isWebScraper);
                if (String.IsNullOrEmpty(data)) throw new Exception("Request returned a null response, IP is possibly blocked.");
            }
            return data;
        }

        public static string MakeApiRequest(string url)
        {
            string motData;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = SetUserAgent(false);
            request.Timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            request.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-api-key", ApiKey);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException("Stream returns null.")))
                    {
                        motData = reader.ReadToEnd();
                    }
                }
            }
            return motData;
        }

        private static string DownloadString(string url, bool useProxy, bool isWebScraper)
        {
            string data;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = SetUserAgent(isWebScraper);
            request.Timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            request.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            request.Method = "GET";

            if (useProxy)
            {
                // If IP and port are not populated, then use the next in the list.
                if (String.IsNullOrEmpty(_ip) && _port == null) Next();

                Log.Info($"Attempting with {_ip}:{_port}");
                Uri uri = new Uri($"http://{_ip}:{_port}");
                WebProxy proxy = new WebProxy(uri, false);
                request.Proxy = proxy;
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException("Stream returns null.")))
                    {
                        data = reader.ReadToEnd();
                    }
                }
            }
            return data;
        }

        private static void Next()
        {
            string ipAndPort = ProxyList.Pop();
            _ip = ipAndPort.Split(':')[0];
            _port = int.Parse(ipAndPort.Split(':')[1]);
        }

        private static IList<string> Shuffle(IList<string> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = rnd.Next(0, n) % n;
                n--;
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        private static string SetUserAgent(bool isWebScraper) => isWebScraper ? UserAgents[0] : UserAgents[new Random().Next(UserAgents.Count)];
    }
}
