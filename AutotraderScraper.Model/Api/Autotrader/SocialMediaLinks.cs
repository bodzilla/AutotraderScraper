using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class SocialMediaLinks : IBaseModel
    {
        public SocialMediaLinks()
        {
            VirtualAdverts = new HashSet<Advert>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("twitter", NullValueHandling = NullValueHandling.Ignore)]
        public string Twitter { get; set; }

        [JsonProperty("facebook", NullValueHandling = NullValueHandling.Ignore)]
        public string Facebook { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        public virtual ICollection<Advert> VirtualAdverts { get; set; }
    }
}
