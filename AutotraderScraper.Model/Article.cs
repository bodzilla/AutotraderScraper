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

        public DateTime? DateEnded { get; set; } = null;

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
    }
}
