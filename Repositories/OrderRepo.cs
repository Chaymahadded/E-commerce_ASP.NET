using InventoryBeginners.Data;
using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryBeginners.Repositories
{
    
    public class OrderRepo : IOrder
    {
        private readonly InventoryContext _context; // for connecting to efcore.
        public OrderRepo(InventoryContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public void order(ShoppingCart cart, string userId)
        {
            // Create a new ShoppingCart entity without setting its primary key
            ShoppingCart newCart = new ShoppingCart
            {
                Etat = "EnCours",
                UserId = cart.UserId,
                Total= cart.Total
            };

            // Add the new ShoppingCart entity to the context
            _context.ShoppingCarts.Add(newCart);

            // Set the ShoppingCartId of each CartItem to null to allow the database to generate it
            foreach (var cartItem in cart.Items)
            {
                cartItem.ShoppingCartId = newCart.ShoppingCartId;
            }

            //// Add the CartItems to the context
            _context.CartItems.AddRange(cart.Items);
          

            // Save changes to persist the new ShoppingCart and associated CartItems
            _context.SaveChanges();

          
            
        }
        public List<ShoppingCart> GetAllOrders()
        {
            List<ShoppingCart> c = _context.ShoppingCarts.ToList();
            foreach(ShoppingCart cart in c)
            {
                cart.UserId = (_context.Users
                  .Where(n => n.Id == cart.UserId)
                  .FirstOrDefault()).UserName;


            }
            return c;
        }

        public List<ShoppingCart> GetOrdersByUser(string userId)
        {
            List<ShoppingCart> c = _context.ShoppingCarts.ToList();
            List<ShoppingCart> carts = new List<ShoppingCart> ();
            foreach(ShoppingCart cart in c)
            {

                if (string.Equals(cart.UserId, userId, StringComparison.OrdinalIgnoreCase))
                {
                    carts.Add(cart);
                }
            }
            return carts;
        }


    }
}
