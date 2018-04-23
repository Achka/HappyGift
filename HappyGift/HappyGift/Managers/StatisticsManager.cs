using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyGift.Data;
using HappyGift.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyGift.Managers
{
    public class StatisticsManager : IStatisticsManager
    {

        private readonly ApplicationDbContext _contex;
        public StatisticsManager(ApplicationDbContext context)
        {
            _contex = context;
        }
        
        public string GetMostPopularService()
        {
            var service = _contex.Services.Include(s => s.GiftServices).ThenInclude(gs => gs.Service).
                OrderByDescending(ser => ser.GiftServices.Count).FirstOrDefault();
            return service?.Name;
        }

        public string GetLeastPopularService()
        {
            var service = _contex.Services.Include(s => s.GiftServices).ThenInclude(gs => gs.Service).
                OrderBy(ser => ser.GiftServices.Count).FirstOrDefault();
            return service?.Name;
        }

        public decimal GetTotalPrice()
        {
            return _contex.Gifts.Include(g => g.GiftServices).ThenInclude(gs => gs.Service)
                .Where(gift => gift.IsAcceptedByAdmin).Sum(gif => gif.GiftServices.Sum(gis => gis.Service.Price));
        }
    }
}
