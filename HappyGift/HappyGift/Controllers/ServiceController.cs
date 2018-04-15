using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;
using System.Threading.Tasks;
using HappyGift.Models.ServiceViewModels;
using HappyGift.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

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
            CreateServiceViewModel model = new CreateServiceViewModel();
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
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult CreateService(int? id)
        {
            return View();
        }

        
        public IActionResult DeleteService(long serviceId)
        {
            var service = _context.Services.Where(c => c.Id == serviceId).FirstOrDefault();
            
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
