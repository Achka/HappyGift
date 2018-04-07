using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;
using Microsoft.AspNetCore.Identity;
using HappyGift.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HappyGift.Controllers
{
    public class GiftController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<HappyGiftUser> _userManager;

        public GiftController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager)
        {

            _context = context;
            _userManager = userManager;


            
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var id = currentUser.Id;

            //_context.Add(new Gift
            //{
            //    UserId = id,
            //    IsAcceptedByAdmin = false
            //});
            //_context.Add(new Gift
            //{
            //    UserId = id,
            //    IsAcceptedByAdmin = false
            //});

            //_context.Add(new Gift
            //{
            //    UserId = id,
            //    IsAcceptedByAdmin = true
            //});

            //_context.Add(new Gift
            //{
            //    UserId = id,
            //    IsAcceptedByAdmin = true
            //});

            //_context.Add(new GiftServices
            //{
            //    GiftId = 1,
            //    ServiceId = 1
            //});

            //_context.Add(new GiftServices
            //{
            //    GiftId = 1,
            //    ServiceId = 2
            //});

            //_context.Add(new GiftServices
            //{
            //    GiftId = 2,
            //    ServiceId = 3
            //});

            //_context.Add(new GiftServices
            //{
            //    GiftId = 2,
            //    ServiceId = 4
            //});

            //_context.Add(new GiftServices
            //{
            //    GiftId = 3,
            //    ServiceId = 2
            //});


            //_context.Add(new GiftServices
            //{
            //    GiftId = 4,
            //    ServiceId = 4
            //});

            _context.SaveChanges();
           var d = _context.Gifts.Include(g => g.GiftServices);
            ViewData["Gifts"] = _context.Gifts.Include(g => g.GiftServices).Where(g => g.UserId == currentUser.Id && !g.IsAcceptedByAdmin)
                                              .Select(g => g.GiftServices.Select(s => s.Service.Name).ToList())
                                              .ToList();


            return View();
        }

    }
}
