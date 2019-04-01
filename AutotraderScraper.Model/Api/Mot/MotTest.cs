using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Mot
{
    [JsonObject("motTests", ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MotTest : IBaseModel
    {
        public MotTest()
        {
            VirtualRfrAndComments = new HashSet<RfrAndComment>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int? MotResponseId { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string TestResult { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? OdometerValue { get; set; }

        public string OdometerUnit { get; set; }

        public long MotTestNumber { get; set; }

        [JsonProperty("rfrAndComments", NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<RfrAndComment> VirtualRfrAndComments { get; set; }

        [ForeignKey("MotResponseId")]
        public virtual MotResponse MotResponse { get; set; }
    }
}
