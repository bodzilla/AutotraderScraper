using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class SocialMediaLinksRepository : IGenericRepository<SocialMediaLinks>
    {
        private readonly GenericRepository<SocialMediaLinks> _repository;

        public SocialMediaLinksRepository()
        {
            _repository = new GenericRepository<SocialMediaLinks>();
        }

        public IList<SocialMediaLinks> GetAll(params Expression<Func<SocialMediaLinks, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<SocialMediaLinks> GetList(Func<SocialMediaLinks, bool> where, params Expression<Func<SocialMediaLinks, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public SocialMediaLinks Get(Func<SocialMediaLinks, bool> where, params Expression<Func<SocialMediaLinks, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<SocialMediaLinks, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params SocialMediaLinks[] socialMediaLinks)
        {
            _repository.Create(socialMediaLinks);
        }

        public void Update(params SocialMediaLinks[] socialMediaLinks)
        {
            _repository.Update(socialMediaLinks);
        }

        public void Delete(params SocialMediaLinks[] socialMediaLinks)
        {
            _repository.Delete(socialMediaLinks);
        }
    }
}
