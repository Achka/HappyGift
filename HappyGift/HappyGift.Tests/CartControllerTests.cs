using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using HappyGift.Controllers;
using HappyGift.Data;
using HappyGift.Managers;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using HappyGift.Models.CartViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
namespace HappyGift.Tests
{
    [TestFixture]
    public class CartControllerTests :BaseTests
    {
        private ICartManager _cartManager;
        private ApplicationDbContext _context;
        private CartController _controller;
        [SetUp]
        public void Init()
        {
            var options = GetContextOptions();
            _context = new ApplicationDbContext(options);
            _cartManager = new CartManager(_context);
            CreateFakeUser(_context);
            AddFakeServices(_context);
            //var mockUserManager = new Mock<UserManager<HappyGiftUser>>();
            //var userManger = new UserManager<HappyGiftUser>(new Mock<IUserStore<HappyGiftUser>>().Object,
            //    new Mock<IOptions<IdentityOptions>>().Object,
            //    new Mock<IPasswordHasher<HappyGiftUser>>().Object,
            //    new IUserValidator<HappyGiftUser>[0],
            //    new IPasswordValidator<HappyGiftUser>[0],
            //    new Mock<ILookupNormalizer>().Object,
            //    new Mock<IdentityErrorDescriber>().Object,
            //    new Mock<IServiceProvider>().Object,
            //    new Mock<ILogger<UserManager<HappyGiftUser>>>().Object);
            //mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
               // .Returns(_context.Users.FirstOrDefaultAsync());
            _controller = new CartController(_context, new FakeUserManager());
        }

        [Test]
        public async Task Index_RenderedAsync()
        {
            var result = await _controller.Index();
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as CartListViewModel;
            Assert.IsTrue(model.CartItems.IsNullOrEmpty());
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetNumberOfItemsInCartAsync()
        {
            _context.Carts.Add(new Cart
            {
                CartId = 1,
                UserId = _context.Users.FirstOrDefault().Id,
                CartServices = new List<CartServices>
                {
                    new CartServices
                    {
                        CartId = 1,
                        ServiceId = _context.Services.FirstOrDefault().Id,
                    },
                    new CartServices
                    {
                        CartId = 1,
                        ServiceId = _context.Services.LastOrDefault().Id,
                    }
                }
            });
            _context.SaveChanges();
            var number = await  _controller.GetNumberOfItemsInCart();
            Assert.IsTrue(number == 2);
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task SpecifyCity()
        {
            _context.Carts.Add(new Cart
            {
                CartId = 1,
                UserId = _context.Users.FirstOrDefault().Id,
                CartServices = new List<CartServices>
                {
                    new CartServices
                    {
                        CartServiceId = 1,
                        CartId = 1,
                        ServiceId = _context.Services.FirstOrDefault().Id,
                    },
                    new CartServices
                    {
                        CartServiceId = 2,
                        CartId = 1,
                        ServiceId = _context.Services.LastOrDefault().Id,
                    }
                }
            });
            _context.SaveChanges();
            var result = await _controller.SaveCity("Lviv", 1);
            Assert.IsInstanceOf<ActionResult>(result);

            Assert.IsTrue(_context.Carts.FirstOrDefault(c => c.CartId == 1).City == "Lviv");
            _context.Database.EnsureDeleted();

        }

        [Test]
        public async Task RemoveFromCart()
        {
            _context.Carts.Add(new Cart
            {
                CartId = 1,
                UserId = _context.Users.FirstOrDefault().Id,
                CartServices = new List<CartServices>
                {
                    new CartServices
                    {
                        CartServiceId = 1,
                        CartId = 1,
                        ServiceId = _context.Services.FirstOrDefault().Id,
                    },
                    new CartServices
                    {
                        CartServiceId = 2,
                        CartId = 1,
                        ServiceId = _context.Services.LastOrDefault().Id,
                    }
                }
            });
            _context.SaveChanges();
            var result = await _controller.RemoveFromCart(1);
            Assert.IsInstanceOf<ActionResult>(result);
            
            Assert.IsTrue(_context.Carts.Include(c=>c.CartServices).FirstOrDefault(c =>c.UserId == _context.Users.FirstOrDefault().Id).CartServices.Count() ==1);
            _context.Database.EnsureDeleted();
        }


        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
            _cartManager.Dispose();
        }
    }
}
