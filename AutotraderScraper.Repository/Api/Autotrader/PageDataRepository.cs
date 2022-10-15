using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class PageDataRepository : IGenericRepository<PageData>
    {
        private readonly GenericRepository<PageData> _repository;

        public PageDataRepository()
        {
            _repository = new GenericRepository<PageData>();
        }

        public IList<PageData> GetAll(params Expression<Func<PageData, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<PageData> GetList(Func<PageData, bool> where, params Expression<Func<PageData, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public PageData Get(Func<PageData, bool> where, params Expression<Func<PageData, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<PageData, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params PageData[] pageDatas)
        {
            _repository.Create(pageDatas);
        }

        public void Update(params PageData[] pageDatas)
        {
            _repository.Update(pageDatas);
        }

        public void Delete(params PageData[] pageDatas)
        {
            _repository.Delete(pageDatas);
        }
    }
}
