using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public string Category { get; set; }
        public List<Service> Services { get; set; }
    }
}
