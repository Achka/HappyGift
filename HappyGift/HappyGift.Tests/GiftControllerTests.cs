using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HappyGift.Controllers;
using HappyGift.Data;
using HappyGift.Models.ServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using HappyGift.Managers.Interfaces;
using HappyGift.Managers;

namespace HappyGift.Tests
{
    [TestFixture]
    public class GiftControllerTests : BaseTests
    {
        private ApplicationDbContext _context;
        private GiftController _controller;
        private ICartManager _cartManager;

        [SetUp]
        public void Init()
        {
            var options = GetContextOptions();
            _context = new ApplicationDbContext(options);
            _cartManager = new CartManager(_context);

            AddFakeServices(_context);
            _controller = new GiftController(_context, new FakeUserManager());
        }

        [Test]
        public async Task Index_Rendered()
        {
            var result = await _controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNotNull(result);

            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreateGift_Rendered()
        {
            _cartManager.CreateNewCart(CreateFakeUser(_context));
            var result = await _controller.CreateGift();

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.IsNotNull(result);

            _context.Database.EnsureDeleted();
        }

        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
            _controller.Dispose();
        }
    }
}
