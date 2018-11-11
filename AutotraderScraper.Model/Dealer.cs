using System;
using System.Collections.Generic;
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
