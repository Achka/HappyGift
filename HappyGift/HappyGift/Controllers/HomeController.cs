using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HappyGift.Models;
using HappyGift.Data;
using HappyGift.Models.HomeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HappyGift.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        {}

        public IActionResult Index()
        {
            //Service s = new Service
            //{
            //    Name = "Extreme driving",
            //    Price = 1000,
            //    Description = "The personal lesson of extreme or contra-driving is a chance to improve your driving skills and get the right reaction even in unpredictable road situations. Depending on the level of driving skills, you can learn how to manage properly, go in turns at different speeds, overcome snakes, stop quickly, and navigate sudden obstacles. Honda Civic is available. It is advisable to have leather gloves with you.",
            //    ImageUrl = @"https://www.bodo.ua/upload/files/cm-experience/3/2248/images_file/all_all_small-r1w450h280.jpg"
            //};
            //_context.Add(s);
            //Service s3 = new Service
            //{
            //    Name = "Chocolate massage",
            //    Price = 500,
            //    Description = "Masterful massage of the neck-collar zone, which will allow to plunge into the world of relaxation! In just one session fatigue disappears and the balance of vital forces is restored. The secret of such an effect in an individual combination of techniques, combining techniques of point impact, rubbing and stroking.",
            //    ImageUrl = @"https://www.bodo.ua/upload/files/cm-experience/3/2145/images_file/all_all_small-r1w450h280.jpg"
            //};
            //_context.Add(s3);
            //Service s1 = new Service
            //{
            //    Name = "Plane trip",
            //    Price = 2000,
            //    Description = "Capture and capture the 20-minute stay in the sky on a light single-engined Tecnam or Aquila. Nearby there will be a professional pilot who will tell a lot of interesting about the management of the aircraft. From the incredible beauty of landscapes that stretch below, breathtaking. The flight will be at an altitude of up to 800 m.",
            //    ImageUrl = @"https://www.bodo.ua/upload/files/cm-experience/103/102153/images_file/all_all_small-r1w450h280.jpg"
            //};
            //_context.Add(s1);
            //Service s2 = new Service
            //{
            //    Name = "Horse riding",
            //    Price = 800,
            //    Description = "Riding on the breed horses in the picturesque outskirts of the capital - in the territory of the equestrian club. For experienced riders, nothing prevents going straight for a walk, but the experienced instructor will take care of the newcomers. He will always accompany the couple on a walk. He will take the steps horizontally, keeping them behind the wheel all the time.",
            //    ImageUrl = @"https://www.bodo.ua/upload/files/cm-experience/35/34588/images_file/all_all_small-r1w450h280.jpg"
            //};
            //_context.Add(s2);
            //_context.SaveChanges();
            var model = _context.Services.Include(s => s.Category).Where(s => !s.IsDeleted).ToList()
                .GroupBy(ser => ser.Category).Select(service => new HomeViewModel
            {
                Category = service.Key?.Name ?? "Without category",
                Services = service.ToList()
            }).ToList();
            return View(model);
        }
    }
}
