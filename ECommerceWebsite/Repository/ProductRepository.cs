using System;
using ECommerce.DataAccess.Data;
using ECommerceWebsite.Models;
using ECommerceWebsite.Repository.IRepository;

namespace ECommerceWebsite.Repository;

public class ProductRepository: Repository<Product>,IProductRepository
{
     private ApplicationDBContext _db;
    public ProductRepository(ApplicationDBContext db): base(db)
    {
        _db = db;
    }
   


    public void Update(Product obj)
    {   
        var objFromdb = _db.Product.FirstOrDefault(u => u.Id == obj.Id);
        if(objFromdb != null)
        {
            objFromdb.ProductName = obj.ProductName;
            objFromdb.Brand = obj.Brand;
            objFromdb.Description = obj.Description;
            objFromdb.CategoryId = obj.CategoryId;
            objFromdb.Price = obj.Price;
            objFromdb.ListPrice = obj.ListPrice;
            objFromdb.Price50 = obj.Price50;
            objFromdb.Price100 = obj.Price100;
            if(obj.ImageUrl !=null)
            {
                objFromdb.ImageUrl = obj.ImageUrl;
            }
        }
    }
}
