using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api
{
    public class ApiArticleRepository : IGenericRepository<ApiArticle>
    {
        private readonly GenericRepository<ApiArticle> _repository;

        public ApiArticleRepository()
        {
            _repository = new GenericRepository<ApiArticle>();
        }

        public IList<ApiArticle> GetAll(params Expression<Func<ApiArticle, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<ApiArticle> GetList(Func<ApiArticle, bool> where, params Expression<Func<ApiArticle, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public ApiArticle Get(Func<ApiArticle, bool> where, params Expression<Func<ApiArticle, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<ApiArticle, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params ApiArticle[] apiArticles)
        {
            _repository.Create(apiArticles);
        }

        public void Update(params ApiArticle[] apiArticles)
        {
            _repository.Update(apiArticles);
        }

        public void Delete(params ApiArticle[] apiArticles)
        {
            _repository.Delete(apiArticles);
        }
    }
}
