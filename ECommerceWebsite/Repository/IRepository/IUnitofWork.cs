using System;

namespace ECommerceWebsite.Repository.IRepository;

public interface IUnitofWork
{
    
    ICategoryRepository category {get;}
    IProductRepository product {get;}
    ICompanyRepository company {get;}
    void Save();
}
