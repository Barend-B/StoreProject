using CannyStore.Core.Models;
using CannyStore.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CannyStore.Core.Contracts
{
    public interface ICartService
    {
        void AddToCart(HttpContextBase httpContext, string productId);
        void RemoveFromCart(HttpContextBase httpContext, string itemId);
        List<CartItemVM> GetCartItems(HttpContextBase httpContext);
        CartSummaryVM GetCartSummary(HttpContextBase httpContext);
        void ClearCart(HttpContextBase httpContext);
    }
}
