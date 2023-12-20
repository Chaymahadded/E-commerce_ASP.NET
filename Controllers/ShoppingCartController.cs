using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryBeginners.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart _shoppingCartService;

        public ShoppingCartController(IShoppingCart shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult AddToCart(CartItem item)
        {
            _shoppingCartService.AddItem(item);
            return RedirectToAction("Index","Home");
        }

        public IActionResult RemoveFromCart(string cartItemId)
        {
            _shoppingCartService.RemoveItem(cartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = _shoppingCartService.GetCart();
            return View(cart);
        }



    }
}
