using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Models;
using HappyGift.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyGift.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ApplicationDbContext context, UserManager<HappyGiftUser> userManager) :
            base(context, userManager)
        {
        }
        
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            var model = new UserViewModel();
            model.Users = users;

            return View(model);
        }
    }
}