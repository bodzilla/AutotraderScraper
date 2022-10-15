using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api
{
    public class ApiArticleVersionRepository : IGenericRepository<ApiArticleVersion>
    {
        private readonly GenericRepository<ApiArticleVersion> _repository;

        public ApiArticleVersionRepository()
        {
            _repository = new GenericRepository<ApiArticleVersion>();
        }

        public IList<ApiArticleVersion> GetAll(params Expression<Func<ApiArticleVersion, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<ApiArticleVersion> GetList(Func<ApiArticleVersion, bool> where, params Expression<Func<ApiArticleVersion, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public ApiArticleVersion Get(Func<ApiArticleVersion, bool> where, params Expression<Func<ApiArticleVersion, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<ApiArticleVersion, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params ApiArticleVersion[] apiArticleVersions)
        {
            _repository.Create(apiArticleVersions);
        }

        public void Update(params ApiArticleVersion[] apiArticleVersions)
        {
            _repository.Update(apiArticleVersions);
        }

        public void Delete(params ApiArticleVersion[] apiArticleVersions)
        {
            _repository.Delete(apiArticleVersions);
        }
    }
}
