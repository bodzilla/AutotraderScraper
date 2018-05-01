using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository
{
    public class CarMakeRepository : IGenericRepository<CarMake>
    {
        private readonly GenericRepository<CarMake> _repository;

        public CarMakeRepository()
        {
            _repository = new GenericRepository<CarMake>();
        }

        public IList<CarMake> GetAll(params Expression<Func<CarMake, object>>[] navigationProperties)
        {
            return _repository.GetAll(x => x.VirtualCarModels);
        }

        public IList<CarMake> GetList(Func<CarMake, bool> where, params Expression<Func<CarMake, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public CarMake Get(Func<CarMake, bool> where, params Expression<Func<CarMake, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<CarMake, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params CarMake[] carMakes)
        {
            _repository.Create(carMakes);
        }

        public void Update(params CarMake[] carMakes)
        {
            _repository.Update(carMakes);
        }

        public void Delete(params CarMake[] carMakes)
        {
            _repository.Delete(carMakes);
        }
    }
}
