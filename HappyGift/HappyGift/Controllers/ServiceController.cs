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

        public async Task<IActionResult> Index(long serviceId)
        {
            CreateServiceViewModel model = new CreateServiceViewModel();
            if(serviceId == 0)
            {
                model.Id = 0;
            }
            else // init edit form
            {
                model.Id = serviceId;

                var service = _context.Services.Where(c => c.Id == serviceId).FirstOrDefault();
                
                model.Name = service.Name;
                model.Price = service.Price.ToString();
                model.ImageUrl = service.ImageUrl;
                model.Description = service.Description;
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult CreateService(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateService(CreateServiceViewModel model)
        {    
            if (ModelState.IsValid)
            {
                var service = new Service {
                    Id = model.Id,
                    Name = model.Name,
                    Price = Convert.ToDecimal(model.Price),
                    ImageUrl = model.ImageUrl,
                    Description = model.Description
                };
                if (model.Id == 0)
                {
                    // redirect just for testing
                    return RedirectToAction("Index", "Home");
                    //_context.Add(service);
                }
                else
                {
                    // redirect just for testing
                    return RedirectToAction("Index", "Cart");
                    //_context.Update(service);
                }
                //_context.SaveChanges();
            }    
            return Ok();
        }
    }
}
