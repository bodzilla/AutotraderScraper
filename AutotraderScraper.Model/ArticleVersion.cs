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

        public double? EngineSize { get; set; }

        public int? Bhp { get; set; }

        public string FuelType { get; set; }

        public string SellerType { get; set; }

        public string Location { get; set; }

        public int Price { get; set; }

        public string Updates { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article VirtualArticle { get; set; }

        public override string ToString()
        {
            string str = String.Empty;
            str += $"Id: {Id}{Environment.NewLine}";
            str += $"DateAdded: {DateAdded}{Environment.NewLine}";
            str += $"ArticleId: {ArticleId}{Environment.NewLine}";
            str += $"Version: {Version}{Environment.NewLine}";
            str += $"Title: {Title}{Environment.NewLine}";
            str += $"Teaser: {Teaser}{Environment.NewLine}";
            str += $"Description: {Description}{Environment.NewLine}";
            str += $"Year: {Year}{Environment.NewLine}";
            str += $"BodyType: {BodyType}{Environment.NewLine}";
            str += $"Mileage: {Mileage}{Environment.NewLine}";
            str += $"TransmissionType: {TransmissionType}{Environment.NewLine}";
            str += $"EngineSize: {EngineSize}{Environment.NewLine}";
            str += $"Bhp: {Bhp}{Environment.NewLine}";
            str += $"FuelType: {FuelType}{Environment.NewLine}";
            str += $"SellerType: {SellerType}{Environment.NewLine}";
            str += $"Location: {Location}{Environment.NewLine}";
            str += $"Price: {Price}{Environment.NewLine}";
            str += $"Updates: {Updates}";
            return str;
        }
    }
}
