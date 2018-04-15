using HappyGift.Data;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var cart = _contex.Carts.Include(c => c.CartServices).Single(c => c.CartId == cartId);
            if (cart == null)
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

        public Cart CreateNewCart(string userId)
        {
            var cart = _contex.Carts.Add(new Cart
            {
                UserId = userId,
            });
            _contex.SaveChanges();
            return cart.Entity;
        }

        public Cart GetCartByUserId(string userId)
        {
            var cart = _contex.Carts.Where(c => c.UserId == userId)
                .Include(c=> c.CartServices)
                .ThenInclude(cs => cs.Service)
                .FirstOrDefault();

            foreach(var service in  cart.CartServices)
            {
                if(service.Service.IsDeleted == true)
                {
                    RemoveFromCart(service.CartServiceId, userId);
                }
            }
            return cart;
        }

        public void RemoveFromCart(int cartServiceId, string userId)
        {
            var cart = _contex.Carts.Where(c => c.UserId == userId)
                .Include(c => c.CartServices)
                .ThenInclude(cs => cs.Service)
                .FirstOrDefault();
            var toDelete = cart.CartServices.Where(cs => cs.CartServiceId == cartServiceId).FirstOrDefault();
            var dbEntity = _contex.Entry(toDelete);
            dbEntity.State = EntityState.Deleted;
            _contex.SaveChanges();
        }
    }
}
