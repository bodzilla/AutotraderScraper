using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class ImageUrlsRepository : IGenericRepository<ImageUrls>
    {
        private readonly GenericRepository<ImageUrls> _repository;

        public ImageUrlsRepository()
        {
            _repository = new GenericRepository<ImageUrls>();
        }

        public IList<ImageUrls> GetAll(params Expression<Func<ImageUrls, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<ImageUrls> GetList(Func<ImageUrls, bool> where, params Expression<Func<ImageUrls, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public ImageUrls Get(Func<ImageUrls, bool> where, params Expression<Func<ImageUrls, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<ImageUrls, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params ImageUrls[] imageUrls)
        {
            _repository.Create(imageUrls);
        }

        public void Update(params ImageUrls[] imageUrls)
        {
            _repository.Update(imageUrls);
        }

        public void Delete(params ImageUrls[] imageUrls)
        {
            _repository.Delete(imageUrls);
        }
    }
}
