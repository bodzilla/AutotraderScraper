using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class InstantMessaging : IBaseModel
    {
        public InstantMessaging()
        {
            VirtualAdverts = new HashSet<Advert>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int PreferencesId { get; set; }

        [ForeignKey("PreferencesId")]
        [JsonProperty("preferences", NullValueHandling = NullValueHandling.Ignore)]
        public Preferences Preferences { get; set; }

        public virtual ICollection<Advert> VirtualAdverts { get; set; }
    }
}
