using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;
using System.Threading.Tasks;
using HappyGift.Models.ServiceViewModels;
using HappyGift.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HappyGift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : BaseController
    {
        public ServiceController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        { }

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
                    Name = model.Name,
                    Price = Convert.ToDecimal(model.Price),
                    ImageUrl = model.ImageUrl,
                    Description = model.Description
                };
                _context.Add(service);
                _context.SaveChanges();
                return RedirectToAction("Index","Home");
            }    
            return Ok();
        }
    }
}
