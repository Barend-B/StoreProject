using CannyStore.Core.Models;
using CannyStore.Core.ViewModels;
using System.Collections.Generic;

namespace CannyStore.Services
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<CartItemVM> cartItems);
        List<Order> GetOrderList();
        Order GetOrder(string id);
        void UpdateOrder(Order updatedOrder);
    }
}