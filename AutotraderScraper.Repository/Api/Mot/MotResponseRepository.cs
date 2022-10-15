using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Mot;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Mot
{
    public class MotResponseRepository : IGenericRepository<MotResponse>
    {
        private readonly GenericRepository<MotResponse> _repository;

        public MotResponseRepository()
        {
            _repository = new GenericRepository<MotResponse>();
        }

        public IList<MotResponse> GetAll(params Expression<Func<MotResponse, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<MotResponse> GetList(Func<MotResponse, bool> where, params Expression<Func<MotResponse, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public MotResponse Get(Func<MotResponse, bool> where, params Expression<Func<MotResponse, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<MotResponse, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params MotResponse[] motResponses)
        {
            _repository.Create(motResponses);
        }

        public void Update(params MotResponse[] motResponses)
        {
            _repository.Update(motResponses);
        }

        public void Delete(params MotResponse[] motResponses)
        {
            _repository.Delete(motResponses);
        }
    }
}
