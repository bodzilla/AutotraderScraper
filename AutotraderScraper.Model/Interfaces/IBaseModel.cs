using System;

namespace AutotraderScraper.Model.Interfaces
{
    public interface IBaseModel
    {
        int Id { get; set; }

        DateTime DateAdded { get; set; }
    }
}
