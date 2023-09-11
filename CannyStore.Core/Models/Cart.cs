using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyStore.Core.Models
{
    public class Cart : BaseEntity
    {
        public virtual ICollection<CartItem> CartItems { get; set;}
        public Cart()
        {
            this.CartItems = new List<CartItem>();
        }
    }
}
