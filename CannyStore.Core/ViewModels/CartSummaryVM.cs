using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyStore.Core.ViewModels
{
    public class CartSummaryVM
    {
        public int CartCount { get; set; }
        public decimal CartTotal { get; set; }
        public CartSummaryVM()
        {
            
        }
        public CartSummaryVM(int cartCount, decimal cartTotal)
        {
            this.CartCount = cartCount;
            this.CartTotal = cartTotal;
        }
    }
}
