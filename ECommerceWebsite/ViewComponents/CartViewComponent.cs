using System;
using System.Security.Claims;
using ECommerce.Utility;
using ECommerceWebsite.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.ViewComponents;

public class CartViewComponent : ViewComponent {
 private readonly IUnitofWork _unitOfWork;
     public CartViewComponent(IUnitofWork unitOfWork) {
            _unitOfWork = unitOfWork;
     }   
     public async Task<IViewComponentResult> InvokeAsync(){
         var claimsIdentity = (ClaimsIdentity)User.Identity;
       var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
       
       if(claim != null)
       {
        if(HttpContext.Session.GetInt32(SD.SessionCart) == null)
        {
         HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.cart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
        }
        return View(HttpContext.Session.GetInt32(SD.SessionCart));
        }
       else{
        HttpContext.Session.Clear();
        return View(0);
       }
     }
}