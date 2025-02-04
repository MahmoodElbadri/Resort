﻿using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Application.Common.Interfaces
{
    public interface IVillaRepository
    {
        IEnumerable<Villa> GetAll
            (Expression<Func<Villa,bool>>? predicate = null,string? includeProperties = null);
        Villa Get
            (Expression<Func<Villa,bool>> predicate,string? includeProperties = null);
        void Add(Villa entity);
        void Update(Villa entity);  
        void Remove(Villa entity);
        void Save();
    }
}
