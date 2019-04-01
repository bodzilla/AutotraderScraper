using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Preferences : IBaseModel
    {
        public Preferences()
        {
            VirtualInstantMessagings = new HashSet<InstantMessaging>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("chatEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ChatEnabled { get; set; }

        [JsonProperty("textEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TextEnabled { get; set; }

        public virtual ICollection<InstantMessaging> VirtualInstantMessagings { get; set; }
    }
}
