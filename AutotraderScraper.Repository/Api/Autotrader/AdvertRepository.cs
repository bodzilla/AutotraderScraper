using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class AdvertRepository : IGenericRepository<Advert>
    {
        private readonly GenericRepository<Advert> _repository;

        public AdvertRepository()
        {
            _repository = new GenericRepository<Advert>();
        }

        public IList<Advert> GetAll(params Expression<Func<Advert, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Advert> GetList(Func<Advert, bool> where, params Expression<Func<Advert, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Advert Get(Func<Advert, bool> where, params Expression<Func<Advert, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Advert, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Advert[] adverts)
        {
            _repository.Create(adverts);
        }

        public void Update(params Advert[] adverts)
        {
            _repository.Update(adverts);
        }

        public void Delete(params Advert[] adverts)
        {
            _repository.Delete(adverts);
        }
    }
}
