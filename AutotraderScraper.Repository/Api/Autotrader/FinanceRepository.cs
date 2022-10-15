using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class FinanceRepository : IGenericRepository<Finance>
    {
        private readonly GenericRepository<Finance> _repository;

        public FinanceRepository()
        {
            _repository = new GenericRepository<Finance>();
        }

        public IList<Finance> GetAll(params Expression<Func<Finance, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Finance> GetList(Func<Finance, bool> where, params Expression<Func<Finance, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Finance Get(Func<Finance, bool> where, params Expression<Func<Finance, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Finance, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Finance[] finances)
        {
            _repository.Create(finances);
        }

        public void Update(params Finance[] finances)
        {
            _repository.Update(finances);
        }

        public void Delete(params Finance[] finances)
        {
            _repository.Delete(finances);
        }
    }
}
