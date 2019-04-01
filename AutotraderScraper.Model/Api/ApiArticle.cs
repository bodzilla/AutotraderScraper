using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Model.Api.Mot;
using AutotraderScraper.Model.Interfaces;

namespace AutotraderScraper.Model.Api
{
    public class ApiArticle : IBaseModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int ArticleId { get; set; }

        public int? AutotraderResponseId { get; set; }

        public int? MotResponseId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [ForeignKey("AutotraderResponseId")]
        public virtual AutotraderResponse AutotraderResponse { get; set; }

        [ForeignKey("MotResponseId")]
        public virtual MotResponse MotResponse { get; set; }
    }
}
