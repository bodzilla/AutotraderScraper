using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Ods : IBaseModel
    {
        public Ods()
        {
            VirtualPageDatas = new HashSet<PageData>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("advertId", NullValueHandling = NullValueHandling.Ignore)]
        public long? AdvertId { get; set; }

        [JsonProperty("advertiserId", NullValueHandling = NullValueHandling.Ignore)]
        public int? AdvertiserId { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Postcode { get; set; }

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public string Context { get; set; }

        [JsonProperty("isFinanceContext", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsFinanceContext { get; set; }

        [JsonProperty("deviceUsed", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceUsed { get; set; }

        public virtual ICollection<PageData> VirtualPageDatas { get; set; }
    }
}
