using InventoryBeginners.Models;
using System.Collections.Generic;

namespace InventoryBeginners.Interfaces
{
    public interface IShoppingCart
    {
        void AddItem(CartItem item);
        void RemoveItem(string cartItemId);
        List<CartItem> GetItems();

        ShoppingCart GetCart();
    }
}
