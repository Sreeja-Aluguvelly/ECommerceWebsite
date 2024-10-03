using System;
using ECommerce.DataAccess.Data;
using ECommerce.Utility;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.DbInitializer;

public class DbInitializer : IDbInitializer
{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDBContext db) {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

    public void Initialize()
    {
        try {
                if (_db.Database.GetPendingMigrations().Count() > 0) {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex) { }
             if(!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) // Checks for the role if doesn't exist then creates a new role in if
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            }
            _userManager.CreateAsync(new ApplicationUser {
                    UserName = "testadmin2@gmail.com",
                    Email = "testadmin2@gmail.com",
                    Name = "Admin Sreeja",
                    PhoneNumber = "1132233333",
                    StreetAddress = "testadmin 123 st",
                    State = "KS",
                    PostalCode = "23422",
                    City = "Overland PArk"
                }, "Sreej@123*").GetAwaiter().GetResult();
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "testadmin2@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();


            return;
}
}
        
