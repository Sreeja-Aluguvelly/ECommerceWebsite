using System;
using System.Linq.Expressions;
using ECommerce.DataAccess.Data;
using ECommerce.Models;
using ECommerceWebsite.Repository.IRepository;

namespace ECommerceWebsite.Repository;

public class CategoryRepository :  Repository<Category>,ICategoryRepository
{
    private ApplicationDBContext _db;
    public CategoryRepository(ApplicationDBContext db): base(db)
    {
        _db = db;
    }
   


    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}
