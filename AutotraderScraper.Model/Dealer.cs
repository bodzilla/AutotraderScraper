using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutotraderScraper.Model.Attributes;
using AutotraderScraper.Model.Interfaces;

namespace AutotraderScraper.Model
{
    public class Dealer : IBaseModel
    {
        public Dealer()
        {
            VirtualArticles = new HashSet<Article>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [MaxLength(450)]
        [Index(IsUnique = true)]
        [CaseSensitive]
        public string Name { get; set; }

        public string Logo { get; set; }

        public virtual ICollection<Article> VirtualArticles { get; set; }
    }
}
