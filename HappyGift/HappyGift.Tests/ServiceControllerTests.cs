using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HappyGift.Controllers;
using HappyGift.Data;
using HappyGift.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using HappyGift.Models.ServiceViewModels;

namespace HappyGift.Tests
{
    [TestFixture]
    public class ServiceControllerTests : BaseTests
    {
        private ApplicationDbContext _context;
        private ServiceController _controller;

        [SetUp]
        public void Init()
        {
            var options = GetContextOptions();
            _context = new ApplicationDbContext(options);

            AddFakeServices(_context);
            _controller = new ServiceController(_context, new FakeUserManager());
        }

        [Test]
        public void Index_Rendered()
        {
            var result = _controller.Index(1);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            var model = viewResult?.Model as CreateServiceViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Id, "1");
            _context.Database.EnsureDeleted();
        }

        //[Test]
        //public void CreateCategory_CategoryCreated()
        //{
        //    var model = new CreateCategoryViewModel();
        //    model.Name = "relax";
        //    model.Id = 1;
        //    var result = _controller.CreateCategory(model);
            
        //    Assert.IsInstanceOf<RedirectToActionResult>(result);
        //    Assert.IsTrue(_context.Category.FirstOrDefault(c => c.CategoryId == 1).CategoryId == 1);
            
        //    _context.Database.EnsureDeleted();
            
        //}

        //[Test]
        //public void CreateService_ServiceCreated()
        //{
        //    var result = _controller.CreateService(4);
        //    Assert.IsInstanceOf<ViewResult>(result);

        //    var viewResult = result as ViewResult;

        //    Assert.IsInstanceOf<ViewResult>(result);
        //    Assert.IsNull(viewResult.Model);

        //    _context.Database.EnsureDeleted();
        //}

        [Test]
        public void DeleteService_ServiceToDeleteId_ServiceDeleted()
        {
            long id = 1;
            var result = _controller.DeleteService(id);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            Assert.IsTrue(_context.Services.FirstOrDefault(service => service.Id == 1).IsDeleted);

            _context.Database.EnsureDeleted();
        }


        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
