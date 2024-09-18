using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
     [DisplayName("Category Name")] // This will show category name on UI and takes string input
    public string Name { get; set; }
    [DisplayName("Display Order")]
    [Range(1,100,ErrorMessage ="Display Number must be between 1-100")]
    public int DisplayOrder { get; set; }

}
