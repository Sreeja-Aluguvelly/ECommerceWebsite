using System;
using ECommerce.Models;

namespace ECommerceWebsite.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
    
}
