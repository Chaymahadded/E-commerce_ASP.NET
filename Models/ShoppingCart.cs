using InventoryBeginners.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InventoryBeginners.Models
{
    public class ShoppingCart
    {
        

        [Key]
        public string ShoppingCartId { get; set; }
        public string UserId { get; set; }
        public string Etat { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;

        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Total { get; set; }


    }
}
