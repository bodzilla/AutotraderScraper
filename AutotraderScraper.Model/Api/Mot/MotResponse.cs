using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Mot
{
    public class MotResponse : IBaseModel
    {
        public MotResponse()
        {
            VirtualApiArticles = new HashSet<ApiArticle>();
            VirtualMotTests = new HashSet<MotTest>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public string Registration { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime? FirstUsedDate { get; set; }

        public string FuelType { get; set; }

        public string PrimaryColour { get; set; }

        public int? ManufactureYear { get; set; }

        public int? DvlaId { get; set; }

        public DateTime? MotTestExpiryDate { get; set; }

        [JsonProperty("motTests", NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<MotTest> VirtualMotTests { get; set; }

        public virtual ICollection<ApiArticle> VirtualApiArticles { get; set; }
    }
}
