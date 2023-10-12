using CannyStore.Core.Contracts;
using CannyStore.Core.Models;
using CannyStore.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CannyStore.UI.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;
        IOrderService orderService;
        IRepository<Customer> customers;
        public CartController(ICartService CartService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.cartService = CartService;
            this.orderService = OrderService;
            this.customers = Customers;
        }
        public ActionResult Index()
        {
            var model = cartService.GetCartItems(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToCart(string Id)            
        {
            cartService.AddToCart(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(string Id)
        {
            cartService.RemoveFromCart(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public PartialViewResult CartSummary()
        {
            var cartSummary = cartService.GetCartSummary(this.HttpContext);
            return PartialView(cartSummary);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode = customer.ZipCode,
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }
        [HttpPost]
        public ActionResult CheckOut(Order order) 
        {
            var cartItem = cartService.GetCartItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //process the payment via 3rd Party
            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, cartItem);
            cartService.ClearCart(this.HttpContext);
            return RedirectToAction("Thank you", new {OrderId = order.Id});
        }
        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}