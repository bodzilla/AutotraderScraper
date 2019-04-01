using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Vehicle : IBaseModel
    {
        public Vehicle()
        {
            VirtualAutotraderResponses = new HashSet<AutotraderResponse>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int? KeyFactsId { get; set; }

        [JsonProperty("make", NullValueHandling = NullValueHandling.Ignore)]
        public string Make { get; set; }

        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [ForeignKey("KeyFactsId")]
        [JsonProperty("keyFacts", NullValueHandling = NullValueHandling.Ignore)]
        public virtual KeyFacts KeyFacts { get; set; }

        [JsonProperty("condition", NullValueHandling = NullValueHandling.Ignore)]
        public string Condition { get; set; }

        [JsonProperty("generationId", NullValueHandling = NullValueHandling.Ignore)]
        public string GenerationId { get; set; }

        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public int? Year { get; set; }

        [JsonProperty("vrm", NullValueHandling = NullValueHandling.Ignore)]
        public string Vrm { get; set; }

        [JsonProperty("derivativeId", NullValueHandling = NullValueHandling.Ignore)]
        public string DerivativeId { get; set; }

        [JsonProperty("tax", NullValueHandling = NullValueHandling.Ignore)]
        public int? Tax { get; set; }

        [JsonProperty("co2Emissions", NullValueHandling = NullValueHandling.Ignore)]
        public string Co2Emissions { get; set; }

        public virtual ICollection<AutotraderResponse> VirtualAutotraderResponses { get; set; }
    }
}
