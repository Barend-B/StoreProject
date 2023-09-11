using CannyStore.Core.Contracts;
using CannyStore.Core.Models;
using CannyStore.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CannyStore.Services
{
    public class CartService : ICartService
    {
        IRepository<Product> productContext;
        IRepository<Cart> cartContext;
        public const string CartSessionName = "eCommerce";
        public CartService(IRepository<Product> productContext, IRepository<Cart> cartContext)
        {
            this.productContext = productContext;
            this.cartContext = cartContext;
        }
        private Cart GetCart(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(CartSessionName);
            Cart cart = new Cart();
            if(cookie != null)
            {
                string cartId = cookie.Value;
                if (string.IsNullOrEmpty(cartId))
                {
                    cart = cartContext.Find(cartId);
                }
                else
                {
                    if (createIfNull)
                    {
                        cart = CreateNewCart(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    cart = CreateNewCart(httpContext);
                }
            }
            return cart;
        }

        private Cart CreateNewCart(HttpContextBase httpContext)
        {
            Cart cart = new Cart();
            cartContext.Insert(cart);
            cartContext.Commit();
            HttpCookie cookie = new HttpCookie(CartSessionName);
            cookie.Value = cart.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);
            return cart;
        }
        public void AddToCart(HttpContextBase httpContext, string productId)
        {
            Cart cart = GetCart(httpContext, true);
            CartItem item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if(item == null)
            {
                item = new CartItem()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                cart.CartItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }
            cartContext.Commit();
        }

        public void ClearCart(HttpContextBase httpContext)
        {
            Cart cart = GetCart(httpContext, false); 
            cart.CartItems.Clear();
            cartContext.Commit();
        }

        public CartSummaryVM GetCart(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public List<CartItemVM> GetCartItems(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(HttpContextBase httpContext, string itemId)
        {
            throw new NotImplementedException();
        }
    }
}
