using AutotraderScraper.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace AutotraderScraper.Model
{
    public class CarMake : IBaseModel
    {
        public CarMake()
        {
            VirtualCarModels = new HashSet<CarModel>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CarModel> VirtualCarModels { get; set; }
    }
}
