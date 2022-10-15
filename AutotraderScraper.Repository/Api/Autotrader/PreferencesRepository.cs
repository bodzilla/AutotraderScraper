using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class PreferencesRepository : IGenericRepository<Preferences>
    {
        private readonly GenericRepository<Preferences> _repository;

        public PreferencesRepository()
        {
            _repository = new GenericRepository<Preferences>();
        }

        public IList<Preferences> GetAll(params Expression<Func<Preferences, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Preferences> GetList(Func<Preferences, bool> where, params Expression<Func<Preferences, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Preferences Get(Func<Preferences, bool> where, params Expression<Func<Preferences, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Preferences, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Preferences[] preferences)
        {
            _repository.Create(preferences);
        }

        public void Update(params Preferences[] preferences)
        {
            _repository.Update(preferences);
        }

        public void Delete(params Preferences[] preferences)
        {
            _repository.Delete(preferences);
        }
    }
}
