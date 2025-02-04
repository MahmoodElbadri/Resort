using Resort.Application.Common.Interfaces;
using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Add(Villa entity)
        {
            throw new NotImplementedException();
        }

        public Villa Get(Expression<Func<Villa, bool>> predicate, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? predicate = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(Villa entity)
        {
            throw new NotImplementedException();
        }
    }
}
