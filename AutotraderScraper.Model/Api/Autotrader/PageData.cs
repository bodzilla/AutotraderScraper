using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class PageData : IBaseModel
    {
        public PageData()
        {
            VirtualAutotraderResponses = new HashSet<AutotraderResponse>();
            VirtualMetadatas = new HashSet<Metadatum>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual ICollection<AutotraderResponse> VirtualAutotraderResponses { get; set; }

        public int OdsId { get; set; }

        public int TrackingId { get; set; }

        [ForeignKey("OdsId")]
        [JsonProperty("ods", NullValueHandling = NullValueHandling.Ignore)]
        public Ods Ods { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Metadatum> VirtualMetadatas { get; set; }

        [JsonProperty("canonical", NullValueHandling = NullValueHandling.Ignore)]
        public string Canonical { get; set; }

        [ForeignKey("TrackingId")]
        [JsonProperty("tracking", NullValueHandling = NullValueHandling.Ignore)]
        public Tracking Tracking { get; set; }

        [JsonProperty("skipPageViewTracking", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SkipPageViewTracking { get; set; }
    }
}
