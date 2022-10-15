using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Autotrader
{
    public class InstantMessagingRepository : IGenericRepository<InstantMessaging>
    {
        private readonly GenericRepository<InstantMessaging> _repository;

        public InstantMessagingRepository()
        {
            _repository = new GenericRepository<InstantMessaging>();
        }

        public IList<InstantMessaging> GetAll(params Expression<Func<InstantMessaging, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<InstantMessaging> GetList(Func<InstantMessaging, bool> where, params Expression<Func<InstantMessaging, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public InstantMessaging Get(Func<InstantMessaging, bool> where, params Expression<Func<InstantMessaging, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<InstantMessaging, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params InstantMessaging[] instantMessagings)
        {
            _repository.Create(instantMessagings);
        }

        public void Update(params InstantMessaging[] instantMessagings)
        {
            _repository.Update(instantMessagings);
        }

        public void Delete(params InstantMessaging[] instantMessagings)
        {
            _repository.Delete(instantMessagings);
        }
    }
}
