﻿using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll
            (Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);
        T Get
            (Expression<Func<T, bool>> predicate, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
    }
}
