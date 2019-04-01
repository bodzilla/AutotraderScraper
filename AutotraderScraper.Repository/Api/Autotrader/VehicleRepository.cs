using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class VehicleRepository : IGenericRepository<Vehicle>
    {
        private readonly GenericRepository<Vehicle> _repository;

        public VehicleRepository()
        {
            _repository = new GenericRepository<Vehicle>();
        }

        public IList<Vehicle> GetAll(params Expression<Func<Vehicle, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Vehicle> GetList(Func<Vehicle, bool> where, params Expression<Func<Vehicle, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Vehicle Get(Func<Vehicle, bool> where, params Expression<Func<Vehicle, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Vehicle, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Vehicle[] vehicles)
        {
            _repository.Create(vehicles);
        }

        public void Update(params Vehicle[] vehicles)
        {
            _repository.Update(vehicles);
        }

        public void Delete(params Vehicle[] vehicles)
        {
            _repository.Delete(vehicles);
        }
    }
}
