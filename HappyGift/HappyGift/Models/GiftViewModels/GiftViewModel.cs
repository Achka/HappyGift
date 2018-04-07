using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models.GiftViewModels
{
    public class GiftViewModel
    {
        public string UserEmail { get; set; }
        public long GiftId { get; set; }
        public List<string> Services { get; set; }
    }
}
