using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerceWebsite.Repository.IRepository;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ECommerce.Utility;

namespace ECommerceWebsite.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    
    private readonly IUnitofWork _unitOfWork;

    public HomeController(IUnitofWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
       var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
       
       if(claim != null)
       {
         HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.cart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
       }
        IEnumerable<Product> productList = _unitOfWork.product.GetAll(includeProperties: "Category");
        return View(productList);
    }
    public IActionResult Details(int productId)
    {
        Cart cart = new(){
            Product = _unitOfWork.product.Get(u => u.Id == productId, includeProperties: "Category"),
            Count = 1,
            ProductId = productId
        };
        
        return View(cart);
    }
    [HttpPost]
    [Authorize]
     public IActionResult Details(Cart cart)
    {
       var claimsIdentity = (ClaimsIdentity)User.Identity;
       var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
       cart.ApplicationUserId = userId;

       Cart cartFromDb = _unitOfWork.cart.Get(u=>u.ApplicationUserId == userId && u.ProductId == cart.ProductId);
       if(cartFromDb != null)
       {
        cartFromDb.Count += cart.Count;
             _unitOfWork.cart.Update(cartFromDb);
             _unitOfWork.Save();
       }
       else{
             _unitOfWork.cart.Add(cart);
             _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.cart.GetAll(u => u.ApplicationUserId == userId).Count());
       }
      TempData["success"] = "Cart Updated Successfully";

        
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
