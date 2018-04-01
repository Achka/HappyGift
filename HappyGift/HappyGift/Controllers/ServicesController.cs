using Microsoft.AspNetCore.Mvc;
using HappyGift.Data;

namespace HappyGift.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        
        }
    }
}
