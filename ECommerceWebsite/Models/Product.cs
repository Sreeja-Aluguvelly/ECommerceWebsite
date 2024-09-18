using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ECommerceWebsite.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    [DisplayName("Product Name")] 
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }

    [Required]
    [DisplayName("List Price")] 
    [Range(1, 1000)]
    public double ListPrice { get; set; }
    
    [Required]
    [DisplayName("Price for 1-50")] 
    [Range(1, 1000)]
    public double Price { get; set; }

    [Required]
    [DisplayName("Price for 50+")] 
    [Range(1, 1000)]
    public double Price50 { get; set; }

    [Required]
    [DisplayName("Price for 100+")] 
    [Range(1, 1000)]
    public double Price100 { get; set; }
    public int CategoryId { get; set; } // Foreign key declaration
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; } // Foreign key Navigation to category model
    [ValidateNever]
    public string ImageUrl { get; set; }
}
