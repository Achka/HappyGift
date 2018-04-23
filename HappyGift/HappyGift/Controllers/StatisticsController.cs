using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Managers;
using HappyGift.Managers.Interfaces;
using HappyGift.Models;
using HappyGift.Models.StatisticsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                TotalSum = _statisticsManager.GetTotalPrice()
            };
            return View(viewModel);
        }


    }
}