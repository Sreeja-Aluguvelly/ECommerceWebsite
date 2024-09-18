using System;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Repository.IRepository;
public interface ICompanyRepository: IRepository<Company>
{
    void Update(Company obj);
}
