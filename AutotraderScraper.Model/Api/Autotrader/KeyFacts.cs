using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class KeyFacts : IBaseModel
    {
        public KeyFacts()
        {
            VirtualVehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("manufactured-year", NullValueHandling = NullValueHandling.Ignore)]
        public string ManufacturedYear { get; set; }

        [JsonProperty("body-type", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyType { get; set; }

        [JsonProperty("mileage", NullValueHandling = NullValueHandling.Ignore)]
        public string Mileage { get; set; }

        [JsonProperty("engine-size", NullValueHandling = NullValueHandling.Ignore)]
        public string EngineSize { get; set; }

        [JsonProperty("transmission", NullValueHandling = NullValueHandling.Ignore)]
        public string Transmission { get; set; }

        [JsonProperty("fuel-type", NullValueHandling = NullValueHandling.Ignore)]
        public string FuelType { get; set; }

        [JsonProperty("doors", NullValueHandling = NullValueHandling.Ignore)]
        public string Doors { get; set; }

        [JsonProperty("seats", NullValueHandling = NullValueHandling.Ignore)]
        public string Seats { get; set; }

        public virtual ICollection<Vehicle> VirtualVehicles { get; set; }
    }
}
