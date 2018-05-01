using AutotraderScraper.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutotraderScraper.Model
{
    public class ArticleVersion : IBaseModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int ArticleId { get; set; }

        public int Version { get; set; }

        public string Title { get; set; }

        public string Teaser { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string BodyType { get; set; }

        public int? Mileage { get; set; }

        public string TransmissionType { get; set; }

        public double EngineSize { get; set; }

        public int? Bhp { get; set; }

        public string FuelType { get; set; }

        public string SellerType { get; set; }

        public string Location { get; set; }

        public int Price { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article VirtualArticle { get; set; }
    }
}
