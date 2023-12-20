using InventoryBeginners.Data;
using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryBeginners.Repositories
{
    public class ShoppingCartRepo : IShoppingCart
    {
        private readonly List<CartItem> _cartItems = new List<CartItem>();


        public void AddItem(CartItem item)
        {
            bool itemExists = false;

            foreach (CartItem c in _cartItems)
            {
                if (string.Equals(c.ProductCode, item.ProductCode, StringComparison.OrdinalIgnoreCase))
                {
                    c.Quantity += item.Quantity;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                _cartItems.Add(item);
            }
        }


        public void RemoveItem(string cartItemId)
        {
            CartItem itemToRemove = null;
            if (cartItemId.Equals("-1"))
            {
                _cartItems.Clear();
            }
            else
            {
                foreach (CartItem c in _cartItems)
                {
                    if (string.Equals(c.ProductCode, cartItemId, StringComparison.OrdinalIgnoreCase))
                    {
                        itemToRemove = c;
                        break;
                    }
                }
                if (itemToRemove != null)
                {
                    _cartItems.Remove(itemToRemove);
                }

            }
        }

        public List<CartItem> GetItems()
        {
            return _cartItems;
        }

        public ShoppingCart GetCart()
        {
            ShoppingCart cart = new ShoppingCart();
            cart.Items = _cartItems;
           
            return cart;

        }


    }
}
