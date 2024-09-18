using System;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
      void Update(Product obj);
}
