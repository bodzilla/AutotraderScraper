using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Mot;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Mot
{
    public class RfrAndCommentRepository : IGenericRepository<RfrAndComment>
    {
        private readonly GenericRepository<RfrAndComment> _repository;

        public RfrAndCommentRepository()
        {
            _repository = new GenericRepository<RfrAndComment>();
        }

        public IList<RfrAndComment> GetAll(params Expression<Func<RfrAndComment, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<RfrAndComment> GetList(Func<RfrAndComment, bool> where, params Expression<Func<RfrAndComment, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public RfrAndComment Get(Func<RfrAndComment, bool> where, params Expression<Func<RfrAndComment, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<RfrAndComment, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params RfrAndComment[] rfrAndComments)
        {
            _repository.Create(rfrAndComments);
        }

        public void Update(params RfrAndComment[] rfrAndComments)
        {
            _repository.Update(rfrAndComments);
        }

        public void Delete(params RfrAndComment[] rfrAndComments)
        {
            _repository.Delete(rfrAndComments);
        }
    }
}
