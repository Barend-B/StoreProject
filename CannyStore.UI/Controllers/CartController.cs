using CannyStore.Core.Contracts;
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
        public CartController(ICartService CartService )
        {
            this.cartService = CartService;
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
    }
}