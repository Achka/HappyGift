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
using HappyGift.Managers.Interfaces;
using HappyGift.Managers;
using Microsoft.EntityFrameworkCore;
using HappyGift.Mappers;

namespace HappyGift.Controllers
{
    public class GiftController : BaseController
    {
        private readonly ICartManager _cartManager;
        private readonly IGiftManager _giftManager;
        public GiftController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager)
            :base(context, userManager)
        {
            _cartManager = new CartManager(context);
            _giftManager = new GiftManager(context, _cartManager);
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUser();

            var model = _giftManager.GetGiftsByUser(currentUser.Id)
                                              .Select(g => g.ToGiftViewModel())
                                              .ToList();

            var newModel = new GiftHistoryViewModel
            {
                GiftViewModels = model
            };
            return View(newModel);
        }

        public async Task<IActionResult> SortByCity(string city)
        {
            var currentUser = await GetCurrentUser();

            var model = _giftManager.GetGiftsByUser(currentUser.Id)
                .Select(g => g.ToGiftViewModel())
                .Where(x=> x.City == city)
                .ToList();

            var newModel = new GiftHistoryViewModel
            {
                City = city,
                GiftViewModels = model
            };
            return View("Index", newModel);
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
        public IActionResult Approve(long giftId)
        {
            _giftManager.ApproveGift(giftId);
            return RedirectToAction("NotApprovedGifts", "Gift");
        }

        public async Task<IActionResult> CreateGift()
        {
            var currentUser = await GetCurrentUser();
            _giftManager.CreateGiftFromCart(currentUser.Id);
            return RedirectToAction("Index", "Gift");
        }

        private List<GiftViewModel> GetNotApprovedGifts()
        {
            return _giftManager.GetNotApprovedGifts()
                                              .Select(g => g.ToGiftViewModel())
                                              .ToList();
        }

    }
}
