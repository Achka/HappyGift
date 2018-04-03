using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyGift.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<HappyGiftUser> _userManager;

        public BaseController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            //if (HttpContext?.User != null)
            //{
            //    _currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            //}
        }
        protected Task<HappyGiftUser> GetCurrentUser()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
