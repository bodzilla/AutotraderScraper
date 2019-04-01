using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class ImageUrls : IBaseModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int? AdvertId { get; set; }

        [ForeignKey("AdvertId")]
        public Advert Advert { get; set; }

        public string Url { get; set; }
    }
}
