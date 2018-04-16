using HappyGift.Data;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyGift.Managers
{
    public class GiftManager : IGiftManager
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartManager _cartManager;
        public GiftManager(ApplicationDbContext context, ICartManager cartManager )
        {
            _context = context;
            _cartManager = cartManager;
        }

        public void CreateGiftFromCart(string userId)
        {
            var cart = _cartManager.GetCartByUserId(userId);
            var cartServices = cart.CartServices.ToList();

            var giftServices = new List<GiftServices>();
            foreach (var cartService in cartServices)
            {
                giftServices.Add(new GiftServices
                {
                    ServiceId = cartService.ServiceId,
                });
                var dbEntry = _context.Entry(cartService);
                dbEntry.State = EntityState.Deleted;
            }
            var gift = new Gift
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                GiftServices = giftServices,
            };
            _context.Gifts.Add(gift);
            _context.SaveChanges();
        }

        public List<Gift> GetGiftsByUser(string userId)
        {
            return _context.Gifts.Where(g => g.UserId == userId)
                .Include(g => g.GiftServices).ThenInclude(g => g.Service)
                .Include(g => g.User).OrderByDescending(g => g.CreatedDate)
                .ToList();
        }

        public List<Gift> GetNotApprovedGifts()
        {
            return _context.Gifts.Where(g => !g.IsAcceptedByAdmin)
                .Include(g =>  g.GiftServices)
                .ThenInclude(gs => gs.Service)
                .Include(g => g.User).ToList();
        }

        public void ApproveGift(long giftId)
        {
            var gift =_context.Gifts.FirstOrDefault(g => g.Id == giftId);
            gift.IsAcceptedByAdmin = true;
            gift.ExpirationDate = DateTime.Now.AddMonths(6);
            _context.SaveChanges();
        }
    }
}
