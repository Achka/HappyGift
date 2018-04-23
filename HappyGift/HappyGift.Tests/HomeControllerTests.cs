using HappyGift.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using HappyGift.Controllers;
using HappyGift.Models;
using HappyGift.Models.CartViewModels;
using HappyGift.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace HappyGift.Tests
{
    [TestFixture]
    public class HomeControllerTests : BaseTests
    {
        private ApplicationDbContext _context;
        private HomeController _controller;

        [SetUp]
        public void Init()
        {
            var options = GetContextOptions();
            _context = new ApplicationDbContext(options);

            AddFakeServices(_context);
            _controller = new HomeController(_context, new FakeUserManager());
        }

        [Test]
        public void Index_Rendered()
        {
            var result = _controller.Index();
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;

            var model = viewResult?.Model as List<HomeViewModel>;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Count, 1);
            Assert.AreEqual(model.First().Services.Count, 3);
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
