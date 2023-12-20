using InventoryBeginners.Models;
using System.Collections.Generic;

namespace InventoryBeginners.Interfaces
{
    public interface IOrder
    {
        void order(ShoppingCart cart, string userid);
        List<ShoppingCart> GetAllOrders();
        List<ShoppingCart> GetOrdersByUser(string userid);
    }
}
