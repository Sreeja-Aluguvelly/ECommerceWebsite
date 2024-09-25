using System;
using ECommerce.DataAccess.Data;
using ECommerceWebsite.Models;
using ECommerceWebsite.Repository.IRepository;

namespace ECommerceWebsite.Repository;

public class OrderDetailRepository : Repository<OrderDetail>,IOrderDetailRepository
{
    private ApplicationDBContext _db;
    public OrderDetailRepository(ApplicationDBContext db): base(db)
    {
        _db = db;
    }
   


    public void Update(OrderDetail obj)
    {
        _db.OrderDetails.Update(obj);
    }
}

