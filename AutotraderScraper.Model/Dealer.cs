using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; }

        public string Logo { get; set; }

        public virtual ICollection<Article> VirtualArticles { get; set; }

        public override string ToString()
        {
            string str = String.Empty;
            str += $"Id: {Id}{Environment.NewLine}";
            str += $"DateAdded: {DateAdded}{Environment.NewLine}";
            str += $"Name: {Name}{Environment.NewLine}";
            str += $"Logo: {Logo}{Environment.NewLine}";
            return str;
        }
    }
}
