using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Model.Api.Mot;
using AutotraderScraper.Model.Interfaces;

namespace AutotraderScraper.Model.Api
{
    public class ApiArticleVersion : IBaseModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int ArticleVersionId { get; set; }

        public int? AutotraderResponseId { get; set; }

        public int? MotResponseId { get; set; }

        [ForeignKey("ArticleVersionId")]
        public virtual ArticleVersion VirtualArticleVersion { get; set; }

        [ForeignKey("AutotraderResponseId")]
        public virtual AutotraderResponse VirtualAutotraderResponse { get; set; }

        [ForeignKey("MotResponseId")]
        public virtual MotResponse VirtualMotResponse { get; set; }
    }
}
