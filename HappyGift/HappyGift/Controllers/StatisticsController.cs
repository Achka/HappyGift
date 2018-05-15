using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Managers;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using HappyGift.Models.ServiceViewModels;
using HappyGift.Models.StatisticsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HappyGift.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsManager _statisticsManager;

        public StatisticsController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) : 
            base(context, userManager)
        {
            _statisticsManager = new StatisticsManager(_context);
        }
        public IActionResult Index()
        {
            var viewModel = new BaseStatisticsViewModel
            {
                MostPopularService = _statisticsManager.GetMostPopularService(),
                LeastPopularService = _statisticsManager.GetLeastPopularService(),
                TotalSum = _statisticsManager.GetTotalPrice(),
                //Services = _context.Services.Include(s=>s.GiftServices).ThenInclude(gs=>gs.Gift).ThenInclude(g=>g.User).Select(ser=>
                //    new ServiceBaseViewModel
                //    {
                //        ServiceName = ser.Name,
                //        AvarageAgeOfCustomer = Convert.ToInt32(ser.GiftServices.Average(gs => (double)(DateTime.Now.Year - gs.Gift.User.YearOfBirth.GetValueOrDefault(1900)))),
                //        MinAgeOfUser = ser.GiftServices.Min(gs => DateTime.Now.Year - gs.Gift.User.YearOfBirth.GetValueOrDefault(1900)),
                //        MaxAgeOfUser = ser.GiftServices.Max(gs => DateTime.Now.Year - gs.Gift.User.YearOfBirth.GetValueOrDefault(1900))
                //    }).ToList()
                
            };
            return View(viewModel);
        }


    }
}