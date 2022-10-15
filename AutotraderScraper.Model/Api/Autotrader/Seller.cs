using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Seller : IBaseModel
    {
        public Seller()
        {
            VirtualAutotraderResponses = new HashSet<AutotraderResponse>();
        }

        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("isTradeSeller", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsTradeSeller { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public string Products { get; set; }

        [JsonProperty("Longitude", NullValueHandling = NullValueHandling.Ignore)]
        public string Longitude { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public string Latitude { get; set; }

        [JsonProperty("bannerUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string BannerUrl { get; set; }

        [JsonProperty("dealerWebsite", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerWebsite { get; set; }

        [JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
        public int? Distance { get; set; }

        [JsonProperty("emailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress { get; set; }

        [JsonProperty("primaryContactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryContactNumber { get; set; }

        [JsonProperty("secondaryContactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryContactNumber { get; set; }

        [JsonProperty("ratingStars", NullValueHandling = NullValueHandling.Ignore)]
        public string RatingStars { get; set; }

        [JsonProperty("ratingTotalReviews", NullValueHandling = NullValueHandling.Ignore)]
        public int? RatingTotalReviews { get; set; }

        [JsonProperty("isTrustedDealer", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsTrustedDealer { get; set; }

        [JsonProperty("locationMapLink", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationMapLink { get; set; }

        [JsonProperty("directionMapLink", NullValueHandling = NullValueHandling.Ignore)]
        public string DirectionMapLink { get; set; }

        [JsonProperty("townAndDistance", NullValueHandling = NullValueHandling.Ignore)]
        public string TownAndDistance { get; set; }

        [JsonProperty("profileUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ProfileUrl { get; set; }

        [JsonProperty("sellerEmailLink", NullValueHandling = NullValueHandling.Ignore)]
        public string SellerEmailLink { get; set; }

        public virtual ICollection<AutotraderResponse> VirtualAutotraderResponses { get; set; }
    }
}
