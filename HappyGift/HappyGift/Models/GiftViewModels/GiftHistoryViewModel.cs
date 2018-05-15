using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models.GiftViewModels
{
    public class GiftHistoryViewModel
    {
        public string City { get; set; }
        public List<GiftViewModel> GiftViewModels { get; set; }
    }
}
