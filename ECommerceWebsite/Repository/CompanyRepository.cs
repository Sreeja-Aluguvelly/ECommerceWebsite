using System;
using System.Linq.Expressions;
using ECommerce.DataAccess.Data;
using ECommerce.Models;
using ECommerceWebsite.Models;
using ECommerceWebsite.Repository.IRepository;

namespace ECommerceWebsite.Repository;

public class CompanyRepository :  Repository<Company>,ICompanyRepository
{
    private ApplicationDBContext _db;
    public CompanyRepository(ApplicationDBContext db): base(db)
    {
        _db = db;
    }
   


    public void Update(Company obj)
    {
        _db.Companies.Update(obj);
    }
}
