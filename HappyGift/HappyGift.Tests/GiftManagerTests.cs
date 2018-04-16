using System.Collections.Generic;
using System.Linq;
using HappyGift.Data;
using HappyGift.Managers;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HappyGift.Tests
{
    [TestFixture]
    public class GiftManagerTests : BaseTests
    {
        private IGiftManager _giftManager;
        private ApplicationDbContext _context;
        [SetUp]
        public void Init()
        {
            var options = GetContextOptions();
            _context = new ApplicationDbContext(options);
            _giftManager =new GiftManager(_context, new CartManager(_context));
            AddFakeServices(_context);
        }
        [Test]
        public void CreateGiftFromCart_GiftCreatedFromCart()
        {
            var userId = CreateFakeUser(_context);
            _context.Carts.Add(new Cart
            {
                CartId = 1,
                UserId = userId,
                CartServices = new List<CartServices>
                {
                    new CartServices
                    {
                        CartId = 1,
                        ServiceId = _context.Services.FirstOrDefault().Id,
                    }
                }
            });
            _context.SaveChanges();
            _giftManager.CreateGiftFromCart(userId);
            Assert.IsFalse(_context.Carts.Include(c =>c.CartServices).FirstOrDefault(c => c.UserId==userId).CartServices.Any());
            Assert.IsTrue(_context.Gifts.Include(g=>g.GiftServices).Any(g => g.UserId == userId && g.GiftServices.Count == 1));
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void GetGiftsByUser_GiftsGotByUserId()
        {
            var userId = CreateFakeUser(_context);
            
            _context.Gifts.Add(new Gift
            {
                Id = 1,
                UserId = userId,
                GiftServices = new List<GiftServices>
                {
                    new GiftServices
                    {
                        GiftId = 1,
                        ServiceId = _context.Services.FirstOrDefault().Id,
                    }
                }
            });
            _context.Gifts.Add(new Gift
            {
                Id = 2,
                UserId = userId,
                GiftServices = new List<GiftServices>
                {
                    new GiftServices
                    {
                        GiftId = 1,
                        ServiceId = _context.Services.LastOrDefault().Id,
                    }
                }
            });
            _context.SaveChanges();
            var gifts = _giftManager.GetGiftsByUser(userId);
            Assert.IsTrue(gifts.Count(g => g.UserId == userId)==2);
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void ApproveGift_GiftApproved()
        {
            var userId = CreateFakeUser(_context);
            var role = _context.Roles.Add(new IdentityRole
            {
                Id = "0cd950bf-5fc5-4d34-90fc-b695342b2ace",
                Name = "Admin",
            });
            var userRole = _context.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = "0cd950bf-5fc5-4d34-90fc-b695342b2ace",
                UserId = userId
            });
            var gift1 = _context.Gifts.Add(new Gift
            {
                Id = 1,
                UserId = userId,
                GiftServices = new List<GiftServices>
                {
                    new GiftServices
                    {
                        GiftId = 1,
                        ServiceId = _context.Services.FirstOrDefault().Id,
                    }
                }
            });
            var gift2 = _context.Gifts.Add(new Gift
            {
                Id = 2,
                UserId = userId,
                GiftServices = new List<GiftServices>
                {
                    new GiftServices
                    {
                        GiftId = 1,
                        ServiceId = _context.Services.LastOrDefault().Id,
                    }
                }
            });
            _context.SaveChanges();
            _giftManager.ApproveGift(gift1.Entity.Id);
            Assert.IsTrue(_context.Gifts.FirstOrDefault(g => g.Id == gift1.Entity.Id).IsAcceptedByAdmin && !_context.Gifts.FirstOrDefault(g => g.Id == gift2.Entity.Id).IsAcceptedByAdmin);
            _context.Database.EnsureDeleted();
        }
        [TearDown]
        public void Dispose()
        {
            _giftManager.Dispose();
        }

    }
}
