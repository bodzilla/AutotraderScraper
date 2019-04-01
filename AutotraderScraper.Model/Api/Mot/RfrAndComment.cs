using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Interfaces;

namespace AutotraderScraper.Model.Api.Mot
{
    public class RfrAndComment : IBaseModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int MotTestId { get; set; }

        [ForeignKey("MotTestId")]
        public virtual MotTest MotTest { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }
    }
}
