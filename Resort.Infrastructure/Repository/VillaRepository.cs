using Microsoft.EntityFrameworkCore;
using Resort.Application.Common.Interfaces;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Infrastructure.Repository;

public class VillaRepository : IVillaRepository
{
    private readonly ApplicationDbContext _db;

    public VillaRepository(ApplicationDbContext db)
    {
        this._db = db;
    }
    public void Add(Villa entity)
    {
        //you can add it with specifying the DbContext 
        //_db.Villas.Add(entity);
        //or you can add it without specifying the DbContext
        _db.Add(entity);
    }

    public Villa Get(Expression<Func<Villa, bool>> predicate, string? includeProperties = null)
    {
        IQueryable<Villa> query = _db.Villas;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query.FirstOrDefault();
    }

    public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? predicate = null, string? includeProperties = null)
    {
        IQueryable<Villa> query = _db.Villas;
        if(predicate != null)
        {
            query = query.Where(predicate);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query.ToList();
    }

    public void Remove(Villa entity)
    {
        _db.Remove(entity);
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(Villa entity)
    {
        _db.Update(entity);
    }
}
