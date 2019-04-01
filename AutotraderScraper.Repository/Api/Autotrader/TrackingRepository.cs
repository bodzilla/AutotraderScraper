using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class TrackingRepository : IGenericRepository<Tracking>
    {
        private readonly GenericRepository<Tracking> _repository;

        public TrackingRepository()
        {
            _repository = new GenericRepository<Tracking>();
        }

        public IList<Tracking> GetAll(params Expression<Func<Tracking, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<Tracking> GetList(Func<Tracking, bool> where, params Expression<Func<Tracking, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public Tracking Get(Func<Tracking, bool> where, params Expression<Func<Tracking, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<Tracking, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params Tracking[] trackings)
        {
            _repository.Create(trackings);
        }

        public void Update(params Tracking[] trackings)
        {
            _repository.Update(trackings);
        }

        public void Delete(params Tracking[] trackings)
        {
            _repository.Delete(trackings);
        }
    }
}
