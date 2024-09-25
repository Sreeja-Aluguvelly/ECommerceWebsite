using System;
using System.Linq.Expressions;
using ECommerce.DataAccess.Data;
using ECommerceWebsite.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDBContext _db; //Dependency Injection
    internal DbSet<T> dbSet;
    public Repository(ApplicationDBContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>(); //dbSet here is same as _db.Categories
        _db.Product.Include(u => u.Category).Include(u => u.CategoryId); // Include all required properties here by appending .Include
    }
    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query ;
        if(tracked){
                query = dbSet;
        }//quering on dbset
        else{
            query=dbSet.AsNoTracking();
        }

        query = query.Where(filter); // where and then parameter(ex:id)
        if(!string.IsNullOrEmpty(includeProperties))
        {
            foreach(var includeprop in includeProperties
            .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeprop);
            }
        }
        return query.FirstOrDefault(); // First one or return null
    }

    //If category and category id are provided then include those properties
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter,string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if(filter != null){
            query = query.Where(filter);
        }
        if(!string.IsNullOrEmpty(includeProperties))
        {
            foreach(var includeprop in includeProperties
            .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeprop);
            }
        }
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
       dbSet.RemoveRange(entity);
    }

    public void Update(T entity)
    {
        dbSet.Update(entity);
    }
}
