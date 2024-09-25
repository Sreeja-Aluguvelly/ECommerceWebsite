using System;

namespace ECommerceWebsite.Repository.IRepository;

public interface IUnitofWork
{
    
    ICategoryRepository category {get;}
    IProductRepository product {get;}
    ICompanyRepository company {get;}
    ICartRepository cart {get;}
    IApplicationUserRepository applicationUser {get;}
    IOrderDetailRepository orderDetail {get;}
    IOrderHeaderRepository orderHeader {get;}

    void Save();
}
