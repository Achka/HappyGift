using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;
using System.Threading.Tasks;
using HappyGift.Models.ServiceViewModels;
using HappyGift.Models;
using System;

namespace HappyGift.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateService(CreateServiceViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var service = new Service {
                    Name = model.Name,
                    Price = Convert.ToDecimal(model.Price),
                    ImageUrl = model.ImageUrl,
                    Description = model.Description
                };

                //_context.Add(service);
                //_context.SaveChanges();
            }
            
            return View(model);
        }
    }
}
