using AutotraderScraper.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutotraderScraper.Model
{
    public class Article : IBaseModel
    {
        public Article()
        {
            VirtualArticleVersions = new HashSet<ArticleVersion>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public bool Active { get; set; } = true;

        public string PriceTag { get; set; }

        public string TagLine { get; set; }

        [MaxLength(450)]
        [Index(IsUnique = true)]
        public string Link { get; set; }

        public string Thumbnail { get; set; }

        public int MediaCount { get; set; } = 0;

        public int? DealerId { get; set; }

        public int CarModelId { get; set; }

        [ForeignKey("DealerId")]
        public Dealer VirtualDealer { get; set; }

        [ForeignKey("CarModelId")]
        public virtual CarModel VirtualCarModel { get; set; }

        public virtual ICollection<ArticleVersion> VirtualArticleVersions { get; set; }

        public override string ToString()
        {
            string str = String.Empty;
            str += $"Id: {Id}{Environment.NewLine}";
            str += $"DateAdded: {DateAdded}{Environment.NewLine}";
            str += $"Active: {Active}{Environment.NewLine}";
            str += $"PriceTag: {PriceTag}{Environment.NewLine}";
            str += $"TagLine: {TagLine}{Environment.NewLine}";
            str += $"Link: {Link}{Environment.NewLine}";
            str += $"Thumbnail: {Thumbnail}{Environment.NewLine}";
            str += $"MediaCount: {MediaCount}{Environment.NewLine}";
            str += $"DealerId: {DealerId}{Environment.NewLine}";
            str += $"CarModelId: {CarModelId}";
            return str;
        }
    }
}
