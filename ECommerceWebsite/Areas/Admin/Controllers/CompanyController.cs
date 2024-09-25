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
    [Authorize(Roles =SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public CompanyController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: CompanyController
        public ActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.company.GetAll().ToList();
            return View(objCompanyList);
        }
        // If nothing is given it is GET method
        public ActionResult Upsert(int? id) //Update and Insert = Upsert
        {
            if(id == null || id == 0)
            {
                //Create 
                return View(new Company());
            }
            else
            {
                //update functionality
               Company companyobj = _unitOfWork.company.Get(u=>u.Id==id);
                return View(companyobj);
            }
            
        }

        [HttpPost]
        public ActionResult Upsert(Company companyobj) // This method will be triggered when create button is clicked and posts the Product properties info into this method.
        {
            
           if(ModelState.IsValid) {
            if(companyobj.Id == 0)
            {
                _unitOfWork.company.Add(companyobj); //keeps track of changes
            }
            else{
                _unitOfWork.company.Update(companyobj);
            }
            _unitOfWork.Save();
            TempData["success"] = "Company Created Successfully";
            return RedirectToAction("Index"); 
            } // goes to database and saves the changes
            else // To not get exception when model is not valid and then populate the dropdown
            {
            return View(companyobj);
            }
             // redirects to index in this controller, if in different controller then add name of the controller as next parameter
        }
        # region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
             List<Company> objCompanyList = _unitOfWork.company.GetAll().ToList();
            return Json(new {data = objCompanyList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.company.Get(u=>u.Id==id);   
            if(companyToBeDeleted == null)
            {
                return Json(new {success=false, message = "Error while deleting"});
            }
                    _unitOfWork.company.Remove(companyToBeDeleted);
                    _unitOfWork.Save();
            return Json(new {success=true, message = "Delete Successful"});
        }
        #endregion
       
    }
        
        
    }
