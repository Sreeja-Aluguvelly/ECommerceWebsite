using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerce.DataAccess.Data;
using ECommerce.Models;
using ECommerce.Utility;
using ECommerceWebsite.Models;
using ECommerceWebsite.Models.ViewModels;
using ECommerceWebsite.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles =SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //Use this to inject image file into wwwroot
        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
        }
        // If nothing is given it is GET method
        public ActionResult Upsert(int? id) //Update and Insert = Upsert
        {
            ProductVM productVM = new ()
            {
            CategoryList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }), // we are getting category type here, Need to convert it to selectlistitem type using projections
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //Create 
                return View(productVM);
            }
            else
            {
                //update functionality
                productVM.Product = _unitOfWork.product.Get(u=>u.Id==id);
                return View(productVM);
            }
            
        }

        [HttpPost]
        public ActionResult Upsert(ProductVM productVM, IFormFile? file) // This method will be triggered when create button is clicked and posts the Product properties info into this method.
        {
            
           if(ModelState.IsValid) {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images/product");
                if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('/')); //combines and gives the path of old image
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create)) 
                {
                    file.CopyTo(fileStream);
                }
                productVM.Product.ImageUrl = @"/images/product/" + fileName;
            }
            if(productVM.Product.Id == 0)
            {
                _unitOfWork.product.Add(productVM.Product); //keeps track of changes
            }
            else{
                _unitOfWork.product.Update(productVM.Product);
            }
            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index"); 
            } // goes to database and saves the changes
            else // To not get exception when model is not valid and then populate the dropdown
            {
            productVM.CategoryList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }); // we are getting category type here, Need to convert it to selectlistitem type using projections
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            return View(productVM);
            }
             // redirects to index in this controller, if in different controller then add name of the controller as next parameter
        }
        # region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.product.GetAll(includeProperties:"Category").ToList();
            return Json(new {data = objProductList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.product.Get(u=>u.Id==id);   
            if(productToBeDeleted == null)
            {
                return Json(new {success=false, message = "Error while deleting"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, 
                            productToBeDeleted.ImageUrl.TrimStart('/')); //combines and gives the path of old image
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    _unitOfWork.product.Remove(productToBeDeleted);
                    _unitOfWork.Save();
            return Json(new {success=true, message = "Delete Successful"});
        }
        #endregion
       
    }
        
        
    }
