using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerceWebsite.Repository.IRepository;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitofWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger,IUnitofWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitOfWork.product.GetAll(includeProperties: "Category");
        return View(productList);
    }

     public IActionResult Details(int id)
    {
       Product product = _unitOfWork.product.Get(u=>u.Id==id,includeProperties: "Category");
        return View(product);
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
