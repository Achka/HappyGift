using HappyGift.Data;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Managers
{
    public class CartManager : ICartManager
    {
        private readonly ApplicationDbContext _contex;
        public CartManager(ApplicationDbContext context)
        {
            _contex = context;
        }
        public bool AddServiceToCart(int serviceId, int cartId)
        {
            var cart =_contex.Carts.Where(c => c.CartId == cartId).FirstOrDefault();
            if(cart == null)
            {
                return false;
            }
            cart.CartServices.Add(new CartServices
            {
                ServiceId = serviceId,
                CartId = cartId
            });
            _contex.SaveChanges();
            return true;
        }

        public bool CreateNewCart(string userId)
        {
            _contex.Carts.Add(new Cart
            {
                UserId = userId,
            });
            _contex.SaveChanges();
            return true;
        }

        public Cart GetCartByUserId(string userId)
        {
            return _contex.Carts.Where(c => c.UserId == userId).FirstOrDefault();
        }
    }
}
