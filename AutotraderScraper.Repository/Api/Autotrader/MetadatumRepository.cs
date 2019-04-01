using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class MetadatumRepository : IGenericRepository<Metadatum>
    {
        private readonly GenericRepository<Metadatum> _repository;

        public MetadatumRepository()
        {
            _repository = new GenericRepository<Metadatum>();
        }

        public IList<Metadatum> GetAll(params Expression<Func<Metadatum, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Metadatum> GetList(Func<Metadatum, bool> where, params Expression<Func<Metadatum, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Metadatum Get(Func<Metadatum, bool> where, params Expression<Func<Metadatum, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Metadatum, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Metadatum[] metadatums)
        {
            _repository.Create(metadatums);
        }

        public void Update(params Metadatum[] metadatums)
        {
            _repository.Update(metadatums);
        }

        public void Delete(params Metadatum[] metadatums)
        {
            _repository.Delete(metadatums);
        }
    }
}
