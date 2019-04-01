using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutotraderScraper.Model.Api.Mot;
using AutotraderScraper.Repository.Interfaces;

namespace AutotraderScraper.Repository.Api.Mot
{
    public class MotTestRepository : IGenericRepository<MotTest>
    {
        private readonly GenericRepository<MotTest> _repository;

        public MotTestRepository()
        {
            _repository = new GenericRepository<MotTest>();
        }

        public IList<MotTest> GetAll(params Expression<Func<MotTest, object>>[] navigationProperties)
        {
            return _repository.GetAll(navigationProperties);
        }

        public IList<MotTest> GetList(Func<MotTest, bool> where, params Expression<Func<MotTest, object>>[] navigationProperties)
        {
            return _repository.GetList(where, navigationProperties);
        }

        public MotTest Get(Func<MotTest, bool> where, params Expression<Func<MotTest, object>>[] navigationProperties)
        {
            return _repository.Get(where, navigationProperties);
        }

        public bool Exists(Func<MotTest, bool> where)
        {
            return _repository.Exists(where);
        }

        public void Create(params MotTest[] motTests)
        {
            _repository.Create(motTests);
        }

        public void Update(params MotTest[] motTests)
        {
            _repository.Update(motTests);
        }

        public void Delete(params MotTest[] motTests)
        {
            _repository.Delete(motTests);
        }
    }
}
