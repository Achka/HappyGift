using HappyGift.Data;
using HappyGift.Managers;
using NUnit.Framework;
using System.Linq;

namespace HappyGift.Tests
{
    [TestFixture]
    public class CartManagerTests : BaseTests
    {
        public CartManagerTests()
        {
            
        }

        [Test]
        public void CreateNewCart_NewCart_CartSuccessfullyAdded()
        {
            //Arrange
            var options = GetContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var cartManager = new CartManager(context);
                var userId = CreateFakeUser(context);

                //Act
                var cart = cartManager.CreateNewCart(userId);

                //Assert
                Assert.True(context.Carts.Any(c => c.UserId == userId));
                Assert.AreEqual(context.Carts.Count(c => c.UserId == userId), 1);
                Assert.AreEqual(context.Carts.FirstOrDefault(c => c.UserId == userId), cart);

                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void GetCartByUserId_ValidUserId_UserCart()
        {
            //Arrange
            var options = GetContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var cartManager = new CartManager(context);
                var userId = CreateFakeUser(context);
                cartManager.CreateNewCart(userId);

                //Act
                var cart = cartManager.GetCartByUserId(userId);

                //Assert
                Assert.IsNotNull(cart);
                Assert.IsTrue(cart.UserId == userId);

                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void GetCartByUserId_InvalidUserId_UserCart()
        {
            //Arrange
            var options = GetContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var cartManager = new CartManager(context);
                var userId = "abcd";

                //Act
                var cart = cartManager.GetCartByUserId(userId);

                //Assert
                Assert.IsNull(cart);

                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void AddServiceToCart_NewService_ServiceSuccessfullyAdded()
        {
            //Arrange
            var options = GetContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var cartManager = new CartManager(context);
                var userId = CreateFakeUser(context);
                var cart = cartManager.CreateNewCart(userId);
                AddFakeServices(context);
                var serviceId = 1;

                //Act
                var result = cartManager.AddServiceToCart(serviceId, cart.CartId);

                //Assert
                var cartWithService = cartManager.GetCartByUserId(userId);
                Assert.IsTrue(result);
                Assert.AreEqual(cartWithService.CartServices.Count(), 1);
                Assert.AreEqual(cartWithService.CartServices.First().ServiceId, serviceId);

                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public void RemoveServiceFromCart_ServiceToRemove_ServiceSuccessfullyRemoved()
        {
            //Arrange
            var options = GetContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var cartManager = new CartManager(context);
                var userId = CreateFakeUser(context);
                var cart = cartManager.CreateNewCart(userId);
                AddFakeServices(context);
                cartManager.AddServiceToCart(1, cart.CartId);
                cartManager.AddServiceToCart(2, cart.CartId);
                var serviceToRemoveId = 1;
                var remainingServiceId = 2;
                var cartWithService = cartManager.GetCartByUserId(userId);
                var cartServiceToRemove = cartWithService.CartServices.FirstOrDefault(s => s.ServiceId == serviceToRemoveId);

                //Act
                cartManager.RemoveFromCart(cartServiceToRemove.CartServiceId, userId);

                //Assert
                cartWithService = cartManager.GetCartByUserId(userId);

                Assert.AreEqual(cartWithService.CartServices.Count, 1);
                Assert.IsTrue(cartWithService.CartServices.Any(service => service.ServiceId == remainingServiceId));
                Assert.IsFalse(cartWithService.CartServices.Any(service => service.ServiceId == serviceToRemoveId));

                context.Database.EnsureDeleted();
            }
        }

       
    }
}
