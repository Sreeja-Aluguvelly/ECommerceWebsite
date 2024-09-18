using System.ComponentModel.DataAnnotations;
using ECommerce.DataAccess.Data;
using ECommerce.Models;
using ECommerce.Utility;
using ECommerceWebsite.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public CategoryController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.category.GetAll().ToList();
            return View(objCategoryList);
        }
        // If nothing is given it is GET method
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category obj) // This method will be triggered when create button is clicked and posts the category properties info into this method.
        {
            if(obj.DisplayOrder.ToString()==obj.Name)
            {
                ModelState.AddModelError("name", "Display Order and Name both cannot be same");
            }
            if(ModelState.IsValid)
            {
            _unitOfWork.category.Add(obj); //keeps track of changes
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index"); 
            } // goes to database and saves the changes
            return View(); // redirects to index in this controller, if in different controller then add name of the controller as next parameter
        }
         public IActionResult Edit(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.category.Get(u=>u.Id==categoryId);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public ActionResult Edit(Category obj)  
        {
            if(ModelState.IsValid)
            {
            _unitOfWork.category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index"); 
            } 
            return View();
        }

        public IActionResult Delete(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.category.Get(u=>u.Id==categoryId);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePOST(int? categoryId) // As get and post has same parameters we have to change the name and explicitly tell that name is delete for this endpoint ( Add actionname)
        {
            Category? obj= _unitOfWork.category.Get(u=>u.Id==categoryId);
            if(obj == null)
            {
                return NotFound();
            }
            _unitOfWork.category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index"); 
        }
       
    }
        
        
    }
