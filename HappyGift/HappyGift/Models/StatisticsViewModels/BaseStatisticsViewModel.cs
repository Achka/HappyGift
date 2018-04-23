using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models.StatisticsViewModels
{
    public class BaseStatisticsViewModel
    {
        public string MostPopularService { get; set; }
        public string LeastPopularService { get; set; }
        public decimal TotalSum { get; set; }
    }
}
