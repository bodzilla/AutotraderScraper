using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;

namespace AutotraderScraper.Model.Api
{
    public class ApiArticle : IBaseModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
