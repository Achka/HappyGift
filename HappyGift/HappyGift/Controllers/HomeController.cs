using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HappyGift.Models;
using HappyGift.Data;
using Microsoft.AspNetCore.Identity;

namespace HappyGift.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        {}

        public IActionResult Index()
        {
            ViewData["Services"] = _context.Services.ToList();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
