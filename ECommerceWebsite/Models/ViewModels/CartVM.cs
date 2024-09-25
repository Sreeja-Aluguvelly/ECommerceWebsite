using System;

namespace ECommerceWebsite.Models.ViewModels;

public class CartVM
{
    public IEnumerable<Cart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
}
