using System;
using System.Linq.Expressions;

namespace ECommerceWebsite.Repository.IRepository;

public interface IRepository<T> where T : class
{
    //T- On Category
    IEnumerable<T> GetAll(Expression<Func<T,bool>>? filter = null,string? includeProperties = null);
    T Get(Expression<Func<T,bool>> filter, string? includeProperties = null, bool tracked = false);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    void RemoveRange(IEnumerable<T> entity);

}
