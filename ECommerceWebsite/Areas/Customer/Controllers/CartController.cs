using System;
using Microsoft.AspNetCore.Mvc;
using ECommerceWebsite.Repository.IRepository;
using ECommerceWebsite.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ECommerceWebsite.Models;
using ECommerce.Utility;
using Stripe.Checkout;

namespace ECommerceWebsite.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController: Controller
{
     private readonly IUnitofWork _unitOfWork;
     [BindProperty] //automatically bind cartvm
     public CartVM CartVM { get; set; }
     public CartController(IUnitofWork unitOfWork) {
            _unitOfWork = unitOfWork;
     }

    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        CartVM = new() {
                CartList = _unitOfWork.cart.GetAll(u => u.ApplicationUserId == userId,includeProperties: "Product"),
                OrderHeader = new()
            };
            foreach (var cart in CartVM.CartList) {
                cart.Price = GetPriceBasedOnQuantity(cart);
                CartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }
        return View(CartVM);
    }

    public IActionResult Plus(int cartId) {
            var cartFromDb = _unitOfWork.cart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.cart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    
    public IActionResult Minus(int cartId) {
            var cartFromDb = _unitOfWork.cart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1) {
                //remove that from cart
                HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.cart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count()-1);
                _unitOfWork.cart.Remove(cartFromDb);
            }
            else {
                cartFromDb.Count -= 1;
                _unitOfWork.cart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId) {
            var cartFromDb = _unitOfWork.cart.Get(u => u.Id == cartId);
            HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.cart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count()-1);
            _unitOfWork.cart.Remove(cartFromDb);
                _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        CartVM = new() {
                CartList = _unitOfWork.cart.GetAll(u => u.ApplicationUserId == userId,includeProperties: "Product"),
                OrderHeader = new()
            };
            CartVM.OrderHeader.ApplicationUser = _unitOfWork.applicationUser.Get(u => u.Id == userId);

            CartVM.OrderHeader.Name = CartVM.OrderHeader.ApplicationUser.Name;
            CartVM.OrderHeader.PhoneNumber = CartVM.OrderHeader.ApplicationUser.PhoneNumber;
            CartVM.OrderHeader.StreetAddress = CartVM.OrderHeader.ApplicationUser.StreetAddress;
            CartVM.OrderHeader.City = CartVM.OrderHeader.ApplicationUser.City;
            CartVM.OrderHeader.State = CartVM.OrderHeader.ApplicationUser.State;
            CartVM.OrderHeader.PostalCode = CartVM.OrderHeader.ApplicationUser.PostalCode;


            foreach (var cart in CartVM.CartList) {
                cart.Price = GetPriceBasedOnQuantity(cart);
                CartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }
        return View(CartVM);
        }
         
        [HttpPost]
        [ActionName("Summary")]
		public IActionResult SummaryPOST() {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            CartVM.CartList = _unitOfWork.cart.GetAll(u => u.ApplicationUserId == userId,includeProperties: "Product");

			CartVM.OrderHeader.OrderDate = System.DateTime.Now;
			CartVM.OrderHeader.ApplicationUserId = userId;

			ApplicationUser applicationUser = _unitOfWork.applicationUser.Get(u => u.Id == userId);


			foreach (var cart in CartVM.CartList) {
				cart.Price = GetPriceBasedOnQuantity(cart);
				CartVM.OrderHeader.OrderTotal += cart.Price * cart.Count;
			}

            if (applicationUser.CompanyId.GetValueOrDefault() == 0) {
				//it is a regular customer 
				CartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				CartVM.OrderHeader.OrderStatus = SD.StatusPending;
			}
            else {
				//it is a company user
				CartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				CartVM.OrderHeader.OrderStatus = SD.StatusApproved;
			}
			_unitOfWork.orderHeader.Add(CartVM.OrderHeader);
			_unitOfWork.Save();
            foreach(var cart in CartVM.CartList) {
                OrderDetail orderDetail = new() {
                    ProductId = cart.ProductId,
                    OrderHeaderId = CartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.orderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            if(applicationUser.CompanyId.GetValueOrDefault() == 0){
                //Customer accout- Capture payment
                //stripe logic
                var domain="http://localhost:5293/";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = domain+$"customer/cart/OrderConfirmation?id={CartVM.OrderHeader.Id}",
                    CancelUrl = domain+"customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };
                foreach(var item in CartVM.CartList)
                {
                    var sessionLineItem = new SessionLineItemOptions{
                        PriceData =  new SessionLineItemPriceDataOptions { 
                            UnitAmount = (long)(item.Price*100), //$10.50 = 1050
                            Currency="usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions{
                                Name = item.Product.ProductName
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.orderHeader.UpdateStripePaymentID(CartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location",session.Url);
            return new StatusCodeResult(303);
            }

			return RedirectToAction(nameof(OrderConfirmation),new { id=CartVM.OrderHeader.Id });
		}

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.orderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            if(orderHeader.PaymentStatus!= SD.PaymentStatusDelayedPayment) {
                //this is an order by customer

                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid") {
					_unitOfWork.orderHeader.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                    _unitOfWork.orderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
				}
                HttpContext.Session.Clear();

			}

            List<Cart> carts = _unitOfWork.cart.GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

            _unitOfWork.cart.RemoveRange(carts);
            _unitOfWork.Save();

            return View(id);
        }

    private double GetPriceBasedOnQuantity(Cart cart) {
            if (cart.Count <= 50) {
                return cart.Product.Price;
            }
            else {
                if (cart.Count <= 100) {
                    return cart.Product.Price50;
                }
                else {
                    return cart.Product.Price100;
                }
            }
        }
}
