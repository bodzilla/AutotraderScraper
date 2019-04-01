using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class AutotraderResponse : IBaseModel
    {
        public AutotraderResponse()
        {
            VirtualApiArticles = new HashSet<ApiArticle>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int? VehicleId { get; set; }

        public int? AdvertId { get; set; }

        public int? SellerId { get; set; }

        public int? FinanceId { get; set; }

        public int? PageDataId { get; set; }

        [ForeignKey("VehicleId")]
        [JsonProperty("vehicle", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Vehicle Vehicle { get; set; }

        [ForeignKey("AdvertId")]
        [JsonProperty("advert", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Advert Advert { get; set; }

        [ForeignKey("SellerId")]
        [JsonProperty("seller", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Seller Seller { get; set; }

        [ForeignKey("FinanceId")]
        [JsonProperty("finance", NullValueHandling = NullValueHandling.Ignore)]
        public Finance Finance { get; set; }

        [ForeignKey("PageDataId")]
        [JsonProperty("pageData", NullValueHandling = NullValueHandling.Ignore)]
        public virtual PageData PageData { get; set; }

        public virtual ICollection<ApiArticle> VirtualApiArticles { get; set; }
    }
}
