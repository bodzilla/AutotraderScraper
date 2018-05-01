﻿using System;
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
    public sealed class Proxy
    {
        private static volatile Proxy _instance;
        private static readonly object Sync = new Object();

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Stack<string> _proxyList = new Stack<string>();
        private readonly bool _useProxy = bool.Parse(ConfigurationManager.AppSettings["UseProxy"]);

        public string Ip { get; set; }

        public int? Port { get; set; }

        public static Proxy Instance
        {
            get
            {
                // Implement Singleton.
                if (_instance != null) return _instance;
                lock (Sync) if (_instance == null) _instance = new Proxy();
                return _instance;
            }
        }

        private Proxy()
        {
            if (!_useProxy) return;

            // Get IP for proxy to use for calls.
            _log.Info("Retrieving proxy list from web..");

            // Get proxy IP from server.
            string proxyUrl = ConfigurationManager.AppSettings["ProxyUrl"];
            int timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(proxyUrl);
            request.UserAgent = ConfigurationManager.AppSettings["UserAgent"];
            request.Timeout = timeout;

            // Web request response will be read into this variable.
            string data;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("Could not get proxy details.", ex.GetBaseException());
                throw;
            }

            MatchCollection regMatch = Regex.Matches(data, @"([0-9]+(?:\.[0-9]+){3}:[0-9]+)");
            if (regMatch.Count > 0)
            {
                IList<string> matches = regMatch.OfType<Match>().Select(m => m.Value).ToList();
                foreach (string match in matches) _proxyList.Push(match);
            }
            else throw new Exception("Proxy list is empty.");
        }

        public string MakeRequest(string url)
        {
            string data = null;

            try
            {
                while (String.IsNullOrEmpty(data))
                {
                    try
                    {
                        data = DownloadString(url);
                    }
                    catch (Exception)
                    {
                        Ip = null;
                        Port = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Could not make request for: {url}", ex.GetBaseException());
            }
            return data;
        }

        private string DownloadString(string url)
        {
            string data;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = ConfigurationManager.AppSettings["UserAgent"];
            request.Timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            request.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMilliSecs"]);
            request.Method = "GET";

            if (_useProxy)
            {
                // If IP and Port are not populated, then use the next in the list.
                if (String.IsNullOrEmpty(Ip) && Port == null)
                {
                    string proxy = _proxyList.Pop();
                    Ip = proxy.Split(':')[0];
                    Port = int.Parse(proxy.Split(':')[1]);
                }

                _log.Info($"Attempting with {Ip}:{Port}");
                Uri uri = new Uri($"http://{Ip}:{Port}");
                WebProxy myproxy = new WebProxy(uri, false);
                request.Proxy = myproxy;
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
            }
            return data;
        }
    }
}
