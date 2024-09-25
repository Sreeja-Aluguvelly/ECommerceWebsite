using System;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Repository.IRepository;

public interface IOrderDetailRepository:IRepository<OrderDetail>
{
    void Update(OrderDetail obj);
    
}
