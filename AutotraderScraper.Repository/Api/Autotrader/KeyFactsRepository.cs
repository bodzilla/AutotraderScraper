using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class KeyFactsRepository : IGenericRepository<KeyFacts>
    {
        private readonly GenericRepository<KeyFacts> _repository;

        public KeyFactsRepository()
        {
            _repository = new GenericRepository<KeyFacts>();
        }

        public IList<KeyFacts> GetAll(params Expression<Func<KeyFacts, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<KeyFacts> GetList(Func<KeyFacts, bool> where, params Expression<Func<KeyFacts, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public KeyFacts Get(Func<KeyFacts, bool> where, params Expression<Func<KeyFacts, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<KeyFacts, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params KeyFacts[] keyFacts)
        {
            _repository.Create(keyFacts);
        }

        public void Update(params KeyFacts[] keyFacts)
        {
            _repository.Update(keyFacts);
        }

        public void Delete(params KeyFacts[] keyFacts)
        {
            _repository.Delete(keyFacts);
        }
    }
}
