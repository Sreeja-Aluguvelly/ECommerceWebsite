using System;
using ECommerce.DataAccess.Data;
using ECommerce.Models;
using ECommerceWebsite.Models;
using ECommerceWebsite.Repository.IRepository;
namespace ECommerceWebsite.Repository;

public class CartRepository: Repository<Cart>,ICartRepository
{
    private ApplicationDBContext _db;
    public CartRepository(ApplicationDBContext db): base(db)
    {
        _db = db;
    }
   


    public void Update(Cart obj)
    {
        _db.carts.Update(obj);
    }
}
