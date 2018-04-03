using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Managers;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using HappyGift.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyGift.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartManager _cartManager;
        public IActionResult Index()
        {
            return View(GetCartsByUser());
        }

        public CartController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        {
            _cartManager = new CartManager(context);
        }
        public async Task<IActionResult> AddToCartAsync(int serviceId)
        {
            var userId = await GetCurrentUser();

            var cart = _cartManager.GetCartByUserId(userId.Id);
            if (cart != null)
            {
                _cartManager.AddServiceToCart(serviceId, cart.CartId);
            }
            else
            {
                _cartManager.CreateNewCart(userId.Id);
            }
            
            //_context.Carts.Add(new Models.Cart { ServiceId = serviceId, UserId = _userManager.GetUserId(HttpContext.User) });
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
        private List<CartItemViewModel> GetCartsByUser()
        {
            var carts =  _context.Carts.Where(cart => cart.UserId == _userManager.GetUserId(HttpContext.User));
            return carts.Select(c =>
                new CartItemViewModel
                {
                    CartId = c.CartId,
                    //ServiceName = c.Service.Name
                }
                ).ToList();
        }
    }
}