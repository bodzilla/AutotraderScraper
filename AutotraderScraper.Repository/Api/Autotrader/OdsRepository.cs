using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class OdsRepository : IGenericRepository<Ods>
    {
        private readonly GenericRepository<Ods> _repository;

        public OdsRepository()
        {
            _repository = new GenericRepository<Ods>();
        }

        public IList<Ods> GetAll(params Expression<Func<Ods, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Ods> GetList(Func<Ods, bool> where, params Expression<Func<Ods, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Ods Get(Func<Ods, bool> where, params Expression<Func<Ods, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Ods, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Ods[] ods)
        {
            _repository.Create(ods);
        }

        public void Update(params Ods[] ods)
        {
            _repository.Update(ods);
        }

        public void Delete(params Ods[] ods)
        {
            _repository.Delete(ods);
        }
    }
}
