using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryBeginners.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _OrderService;
        private readonly IShoppingCart _ShoppingCartService;

        public OrderController(IOrder OrderService, IShoppingCart ShoppingCartService)
        {
            _OrderService = OrderService;
            _ShoppingCartService = ShoppingCartService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Order(ShoppingCart cart,string userId)
        {
            _OrderService.order(cart, userId);
            var orders = _OrderService.GetOrdersByUser(userId);
            _ShoppingCartService.RemoveItem("-1");
            return View("Index", orders);
        }
        public IActionResult OrderList()
        {
            // Récupérer toutes les commandes à partir du service
            var orders = _OrderService.GetAllOrders();

            // Passez les commandes à la vue
            return View(orders);
        }
        public IActionResult OrdersByUser(string userId)
        {
            // Récupérer les commandes pour un utilisateur spécifique
            var orders = _OrderService.GetOrdersByUser(userId);

            // Passez les commandes à la vue
            return View("Index", orders);
        }


    }
}
