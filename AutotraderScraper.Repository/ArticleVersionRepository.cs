using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository
{
    public class ArticleVersionRepository : IGenericRepository<ArticleVersion>
    {
        private readonly GenericRepository<ArticleVersion> _repository;

        public ArticleVersionRepository()
        {
            _repository = new GenericRepository<ArticleVersion>();
        }

        public IList<ArticleVersion> GetAll(params Expression<Func<ArticleVersion, object>>[] navigationProperties)
        {
            return _repository.GetAll(x => x.VirtualArticle);
        }

        public IList<ArticleVersion> GetList(Func<ArticleVersion, bool> where, params Expression<Func<ArticleVersion, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public ArticleVersion Get(Func<ArticleVersion, bool> where, params Expression<Func<ArticleVersion, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<ArticleVersion, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params ArticleVersion[] articleVersions)
        {
            _repository.Create(articleVersions);
        }

        public void Update(params ArticleVersion[] articleVersions)
        {
            _repository.Update(articleVersions);
        }

        public void Delete(params ArticleVersion[] articleVersions)
        {
            _repository.Delete(articleVersions);
        }
    }
}
