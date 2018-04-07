using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;
using Microsoft.AspNetCore.Identity;
using HappyGift.Models;
using Microsoft.AspNetCore.Authorization;
using HappyGift.Models.GiftViewModels;

namespace HappyGift.Controllers
{
    public class GiftController : BaseController
    {
        public GiftController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager)
            :base(context, userManager) {}

        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUser();

            //var id = currentUser.Id;
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

            //_context.SaveChanges();

            var model = _context.Gifts.Where(g => g.UserId == currentUser.Id && g.IsAcceptedByAdmin)
                                              .Select(g => new GiftViewModel
                                              {
                                                  UserEmail = currentUser.Email,
                                                  Services = g.GiftServices.Select(s => s.Service.Name).ToList()
                                              })
                                              .ToList();


            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NotApprovedGifts()
        {
            var model = GetNotApprovedGifts();
            return View("NotApprovedGifts", model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int giftId)
        {
            _context.Gifts.FirstOrDefault(g => g.Id == giftId).IsAcceptedByAdmin = true;
            _context.SaveChanges();

            var model = GetNotApprovedGifts();

            return View("NotApprovedGifts", model);
        }

        private List<GiftViewModel> GetNotApprovedGifts()
        {
            return _context.Gifts.Where(g => !g.IsAcceptedByAdmin)
                                              .Select(g => new GiftViewModel
                                              {
                                                  GiftId = g.Id,
                                                  UserEmail = g.User.Email,
                                                  Services = g.GiftServices.Select(s => s.Service.Name).ToList()
                                              })
                                              .ToList();
        }

    }
}
