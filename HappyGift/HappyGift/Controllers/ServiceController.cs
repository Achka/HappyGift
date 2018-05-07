using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;
using System.Threading.Tasks;
using HappyGift.Models.ServiceViewModels;
using HappyGift.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyGift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : BaseController
    {
        public ServiceController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        { }

        [HttpGet]
        public IActionResult Index(long serviceId)
        {
            var model = new CreateServiceViewModel();
            if(serviceId == 0)
            {
                model.Id = "0";
            }
            else // init edit form
            {
                model.Id = serviceId.ToString();

                var service = _context.Services.Where(c => c.Id == serviceId).FirstOrDefault();
                
                model.Name = service.Name;
                model.Price = service.Price.ToString();
                model.ImageUrl = service.ImageUrl;
                model.Description = service.Description;
                model.Duration = service.Duration;
                model.CategoryId = service.CategoryId.ToString();
            }
            model.Categories = _context.Category.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            });
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateCategory(int? categoryId)
        {
            var model = new CreateCategoryViewModel();
            if (categoryId.HasValue)
            {
                var category = _context.Category.FirstOrDefault(c => c.CategoryId == categoryId);
                model.Name = category.Name;
                model.Id = category.CategoryId;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var category = new Category
                    {
                        Name = model.Name,
                    };
                    _context.Add(category);
                }
                else
                {
                    var category = new Category
                    {
                        CategoryId = model.Id,
                        Name = model.Name,
                    };
                    _context.Update(category);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult DeleteService(long serviceId)
        {
            var service = _context.Services.FirstOrDefault(c => c.Id == serviceId);
            
            service.IsDeleted = true;

            _context.Update(service);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateService(CreateServiceViewModel model)
        {    
            if (ModelState.IsValid)
            {
                if (model.Id == "0")
                {
                    var service = new Service
                    {
                        Name = model.Name,
                        Price = Convert.ToDecimal(model.Price),
                        ImageUrl = model.ImageUrl,
                        Description = model.Description,
                        CategoryId = Convert.ToInt32(model.CategoryId),
                        Duration = model.Duration
                    };
                    _context.Add(service);
                }
                else
                {
                    var service = new Service
                    {
                        Id = Convert.ToInt64(model.Id),
                        Name = model.Name,
                        Price = Convert.ToDecimal(model.Price),
                        ImageUrl = model.ImageUrl,
                        CategoryId = Convert.ToInt32(model.CategoryId),
                        Description = model.Description,
                        Duration = model.Duration
                    };
                    _context.Update(service);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }    
            return Ok();
        }
    }
}
