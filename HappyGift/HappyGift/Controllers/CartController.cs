using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Managers;
using HappyGift.Managers.Interfaces;
using HappyGift.Mappers;
using HappyGift.Models;
using HappyGift.Models.CartViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyGift.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartManager _cartManager;

        public CartController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        {
            _cartManager = new CartManager(context);
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await GetCartsByUser();
            return View(viewModel);
        }

        public async Task<IActionResult> AddToCart(int serviceId)
        {
            var user = await GetCurrentUser();
            var cart = _cartManager.GetCartByUserId(user.Id);
            if (cart == null)
            {
                cart = _cartManager.CreateNewCart(user.Id);
            }
            _cartManager.AddServiceToCart(serviceId, cart.CartId);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveFromCart(int cartServiceId)
        {
            var currentUser = await GetCurrentUser();
            _cartManager.RemoveFromCart(cartServiceId, currentUser.Id);
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> SaveCity(string city, int cartId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == cartId);
            cart.City = city;
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public async Task<int> GetNumberOfItemsInCart()
        {
            var currentUser = await GetCurrentUser();
            return _cartManager.GetCartByUserId(currentUser.Id).CartServices.Count();
        }

        private async Task<CartListViewModel> GetCartsByUser()
        {
            var currentUser = await GetCurrentUser();
            var cart = _cartManager.GetCartByUserId(currentUser.Id);
            return cart.ToCartListViewModel();
        }
    }
}