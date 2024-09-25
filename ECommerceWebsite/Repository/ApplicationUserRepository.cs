using System;
using ECommerce.DataAccess.Data;
using ECommerceWebsite.Models;
using ECommerceWebsite.Repository.IRepository;

namespace ECommerceWebsite.Repository;

public class ApplicationUserRepository:Repository<ApplicationUser>,IApplicationUserRepository
{
    private ApplicationDBContext _db;
    public ApplicationUserRepository(ApplicationDBContext db): base(db)
    {
        _db = db;
    }
}
