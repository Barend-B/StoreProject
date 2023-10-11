using CannyStore.Core.Contracts;
using CannyStore.Core.Models;
using CannyStore.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyStore.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Core.Models.Order> orderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.orderContext = OrderContext;
        }
        public void CreateOrder(Order baseOrder,List<CartItemVM> cartItems) 
        {
            foreach(var item in cartItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                });
            }
            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }
        public Order GetOrder(string id) 
        {
            return orderContext.Find(id);
        }
        public List<Order> GetOrderList()
        {
            return orderContext.Collection().ToList();
        }
        public void UpdateOrder(Order updateOrder) 
        {
            orderContext.Update(updateOrder);
            orderContext.Commit();
        }
    }
}
