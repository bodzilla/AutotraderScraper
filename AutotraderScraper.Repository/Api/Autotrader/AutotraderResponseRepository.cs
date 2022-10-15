using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class AutotraderResponseRepository : IGenericRepository<AutotraderResponse>
    {
        private readonly GenericRepository<AutotraderResponse> _repository;

        public AutotraderResponseRepository()
        {
            _repository = new GenericRepository<AutotraderResponse>();
        }

        public IList<AutotraderResponse> GetAll(params Expression<Func<AutotraderResponse, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<AutotraderResponse> GetList(Func<AutotraderResponse, bool> where, params Expression<Func<AutotraderResponse, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public AutotraderResponse Get(Func<AutotraderResponse, bool> where, params Expression<Func<AutotraderResponse, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<AutotraderResponse, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params AutotraderResponse[] autotraderResponses)
        {
            _repository.Create(autotraderResponses);
        }

        public void Update(params AutotraderResponse[] autotraderResponses)
        {
            _repository.Update(autotraderResponses);
        }

        public void Delete(params AutotraderResponse[] autotraderResponses)
        {
            _repository.Delete(autotraderResponses);
        }
    }
}
