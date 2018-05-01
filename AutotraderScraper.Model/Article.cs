using AutotraderScraper.Model.Interfaces;
using System;
using System.Collections.Generic;
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

        public string Link { get; set; }

        public string Thumbnail { get; set; }

        public int CarModelId { get; set; }

        [ForeignKey("CarModelId")]
        public virtual CarModel VirtualCarModel { get; set; }

        public virtual ICollection<ArticleVersion> VirtualArticleVersions { get; set; }
    }
}
