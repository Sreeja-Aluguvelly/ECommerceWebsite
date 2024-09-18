using System;
using ECommerce.DataAccess.Data;
using ECommerceWebsite.Repository.IRepository;

namespace ECommerceWebsite.Repository;

public class UnitofWork : IUnitofWork
{
     private ApplicationDBContext _db;
     public ICategoryRepository category {get; private set;}
     public IProductRepository product {get; private set;}
      public ICompanyRepository company {get; private set;}
    public UnitofWork(ApplicationDBContext db)
    {
        _db = db;
        category = new CategoryRepository(_db);
        product = new ProductRepository(_db);
        company = new CompanyRepository(_db);
    }
   
    public void Save()
    {
         _db.SaveChanges();
    }
}
