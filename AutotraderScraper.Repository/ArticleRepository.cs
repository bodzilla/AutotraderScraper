using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository
{
    public class ArticleRepository : IGenericRepository<Article>
    {
        private readonly GenericRepository<Article> _repository;

        public ArticleRepository()
        {
            _repository = new GenericRepository<Article>();
        }

        public IList<Article> GetAll(params Expression<Func<Article, object>>[] navigationProperties)
        {
            return _repository.GetAll(x => x.VirtualArticleVersions, x => x.VirtualCarModel);
        }

        public IList<Article> GetList(Func<Article, bool> where, params Expression<Func<Article, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Article Get(Func<Article, bool> where, params Expression<Func<Article, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Article, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Article[] articles)
        {
            _repository.Create(articles);
        }

        public void Update(params Article[] articles)
        {
            _repository.Update(articles);
        }

        public void Delete(params Article[] articles)
        {
            _repository.Delete(articles);
        }
    }
}
