using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class SellerRepository : IGenericRepository<Seller>
    {
        private readonly GenericRepository<Seller> _repository;

        public SellerRepository()
        {
            _repository = new GenericRepository<Seller>();
        }

        public IList<Seller> GetAll(params Expression<Func<Seller, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Seller> GetList(Func<Seller, bool> where, params Expression<Func<Seller, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Seller Get(Func<Seller, bool> where, params Expression<Func<Seller, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Seller, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Seller[] sellers)
        {
            _repository.Create(sellers);
        }

        public void Update(params Seller[] sellers)
        {
            _repository.Update(sellers);
        }

        public void Delete(params Seller[] sellers)
        {
            _repository.Delete(sellers);
        }
    }
}
