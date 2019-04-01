using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Finance : IBaseModel
    {
        public Finance()
        {
            VirtualAutotraderResponses = new HashSet<AutotraderResponse>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }

        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty("providerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderName { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("calculatorLink", NullValueHandling = NullValueHandling.Ignore)]
        public string CalculatorLink { get; set; }

        [JsonProperty("canCalculateMonthlyCost", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CanCalculateMonthlyCost { get; set; }

        [JsonProperty("initialDeposit", NullValueHandling = NullValueHandling.Ignore)]
        public int? InitialDeposit { get; set; }

        public virtual ICollection<AutotraderResponse> VirtualAutotraderResponses { get; set; }
    }
}
