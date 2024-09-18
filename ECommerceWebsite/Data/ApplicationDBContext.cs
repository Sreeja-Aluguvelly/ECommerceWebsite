using System;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ECommerce.DataAccess.Data;

public class ApplicationDBContext : IdentityDbContext // Dbcontext is a builtin class in entityframeworkcore

{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    :base(options) // now register this in program.cs
    {
        
    }

    public DbSet<Category> Categories{ get; set; } // Creates new table Categories with the columns in Category model
    public DbSet<Product> Product{ get; set; }
    public DbSet<Company> Companies{get; set;}
    public DbSet<ApplicationUser> ApplicationUsers{get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) // To Seed data
    {
        base.OnModelCreating(modelBuilder); // add it when we have identitydbcontext
        
        modelBuilder.Entity<Category>().HasData(
            new Category { Id= 1, Name = "Fiction", DisplayOrder = 1 },
            new Category { Id= 2, Name = "Travel", DisplayOrder = 2 },
            new Category { Id= 3, Name = "Music", DisplayOrder = 3 }

        );

        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "Crafted Creations", StreetAddress="112 Tech St", City="Kavrav City",
                                PostalCode="11223", State="IL", PhoneNumber="1122334455"},
            new Company {Id = 2, Name = "Artisan Threads", StreetAddress = "667 Art St", City = "Pandav City",
                                PostalCode = "66778",State = "KS",PhoneNumber = "6677889900"},
            new Company {Id = 3,Name = "Knit & Paint Studio",StreetAddress = "123 Main St",City = "Panchala City",
                                PostalCode = "12345",State = "NY",PhoneNumber = "1234567890"
                }

        );

        modelBuilder.Entity<Product>().HasData(
            new Product { 
                    Id = 1, 
                    ProductName = "CozyWool Knitting Yarn Bundle", 
                    Description = "A soft and warm 100% wool yarn bundle ideal for knitting scarves, sweaters, and blankets. This bundle includes 5 skeins in various colors, providing ample material for multiple projects.",
                    Brand = "CozyWool",
                    ListPrice=49.99,
                    Price=44.99,
                    Price50=42.99,
                    Price100=39.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
            new Product { 
                    Id = 2, 
                    ProductName = "Watercolor Paint Set", 
                    Description = "Premium watercolor paint set with 24 vibrant colors, perfect for both beginners and professionals. The high-pigment paints blend seamlessly, providing rich and smooth coverage for all your watercolor projects.",
                    Brand = "Artist’s Choice",
                    ListPrice=39.99,
                    Price=34.99,
                    Price50=32.99,
                    Price100=30.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
            new Product { 
                    Id = 3, 
                    ProductName = "CandleCraft DIY Candle Making Kit", 
                    Description = "This DIY kit includes everything you need to create your own scented candles at home. The kit comes with soy wax, wicks, essential oils, and reusable containers, making it perfect for crafting unique, personalized candles.",
                    Brand = "CandleCraft",
                    ListPrice=59.99,
                    Price=54.99,
                    Price50=52.99,
                    Price100=49.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
            new Product { 
                    Id = 4, 
                    ProductName = "VibrantColors Acrylic Paint Set", 
                    Description = "A versatile acrylic paint set with 12 tubes of richly pigmented colors, perfect for all types of art projects. These fast-drying paints are suitable for use on canvas, wood, fabric, and more.",
                    Brand = "VibrantColors",
                    ListPrice=29.99,
                    Price=24.99,
                    Price50=22.99,
                    Price100=20.99,
                    CategoryId = 3,
                    ImageUrl = ""
                },
            new Product { 
                    Id = 5, 
                    ProductName = "KnitPro Bamboo Needle Set", 
                    Description = " A set of 10 high-quality bamboo knitting needles in various sizes. Lightweight and smooth, these needles provide a comfortable knitting experience and are ideal for beginners and seasoned knitters alike.",
                    Brand = "KnitPro",
                    ListPrice=39.99,
                    Price=34.99,
                    Price50=32.99,
                    Price100=29.99,
                    CategoryId = 3,
                    ImageUrl = ""
                },
            new Product { 
                    Id = 6, 
                    ProductName = "PureCraft DIY Soap Making Kit", 
                    Description = "Create your own luxurious soaps with this DIY kit. It includes natural soap bases, molds, essential oils, and colorants, allowing you to craft custom soaps at home with ease.",
                    Brand = "PureCraft",
                    ListPrice=49.99,
                    Price=44.99,
                    Price50=42.99,
                    Price100=39.99,
                    CategoryId = 4,
                    ImageUrl = ""
                }
            


        );
    }
}
