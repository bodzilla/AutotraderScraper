using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository
{
    public class DealerRepository : IGenericRepository<Dealer>
    {
        private readonly GenericRepository<Dealer> _repository;

        public DealerRepository()
        {
            _repository = new GenericRepository<Dealer>();
        }

        public IList<Dealer> GetAll(params Expression<Func<Dealer, object>>[] navigationProperties)
        {
            return _repository.GetAll(x => x.VirtualArticles);
        }

        public IList<Dealer> GetList(Func<Dealer, bool> where, params Expression<Func<Dealer, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Dealer Get(Func<Dealer, bool> where, params Expression<Func<Dealer, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Dealer, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Dealer[] dealers)
        {
            _repository.Create(dealers);
        }

        public void Update(params Dealer[] dealers)
        {
            _repository.Update(dealers);
        }

        public void Delete(params Dealer[] dealers)
        {
            _repository.Delete(dealers);
        }
    }
}
