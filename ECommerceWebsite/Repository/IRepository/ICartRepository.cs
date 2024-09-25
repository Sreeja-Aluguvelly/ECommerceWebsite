using System;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Repository.IRepository;

public interface ICartRepository :IRepository<Cart>
{
    void Update(Cart obj);

}
