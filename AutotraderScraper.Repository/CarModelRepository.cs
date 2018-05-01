using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository
{
    public class CarModelRepository : IGenericRepository<CarModel>
    {
        private readonly GenericRepository<CarModel> _repository;

        public CarModelRepository()
        {
            _repository = new GenericRepository<CarModel>();
        }

        public IList<CarModel> GetAll(params Expression<Func<CarModel, object>>[] navigationProperties)
        {
            return _repository.GetAll(x => x.VirtualCarMake);
        }

        public IList<CarModel> GetList(Func<CarModel, bool> where, params Expression<Func<CarModel, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public CarModel Get(Func<CarModel, bool> where, params Expression<Func<CarModel, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<CarModel, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params CarModel[] carModels)
        {
            _repository.Create(carModels);
        }

        public void Update(params CarModel[] carModels)
        {
            _repository.Update(carModels);
        }

        public void Delete(params CarModel[] carModels)
        {
            _repository.Delete(carModels);
        }
    }
}
