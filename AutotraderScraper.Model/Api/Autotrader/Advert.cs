using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Advert : IBaseModel
    {
        public Advert()
        {
            VirtualAutotraderResponses = new HashSet<AutotraderResponse>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int InstantMessagingId { get; set; }

        public int SocialMediaLinksId { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("imageUrls", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ImageUrls { get; set; }

        [ForeignKey("InstantMessagingId")]
        [JsonProperty("instantMessaging", NullValueHandling = NullValueHandling.Ignore)]
        public virtual InstantMessaging InstantMessaging { get; set; }

        [JsonProperty("manufacturerApproved", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ManufacturerApproved { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }

        [JsonProperty("priceValuation", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceValuation { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("attentionGrabber", NullValueHandling = NullValueHandling.Ignore)]
        public string AttentionGrabber { get; set; }

        [JsonProperty("mainImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string MainImageUrl { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public string Products { get; set; }

        [JsonProperty("stockRevisionNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int? StockRevisionNumber { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        [JsonProperty("advertSaved", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AdvertSaved { get; set; }

        [ForeignKey("SocialMediaLinksId")]
        [JsonProperty("socialMediaLinks", NullValueHandling = NullValueHandling.Ignore)]
        public virtual SocialMediaLinks SocialMediaLinks { get; set; }

        [JsonProperty("showDealBuilder", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowDealBuilder { get; set; }

        [JsonProperty("isPartExAvailable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsPartExAvailable { get; set; }

        [JsonProperty("hasWarrantyDirect", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasWarrantyDirect { get; set; }

        [JsonProperty("hasGalleryDealerBanner", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasGalleryDealerBanner { get; set; }

        public virtual ICollection<AutotraderResponse> VirtualAutotraderResponses { get; set; }
    }
}
