using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Managers.Interfaces
{
    public interface IStatisticsManager
    {
        string GetMostPopularService();
        string GetLeastPopularService();
        decimal GetTotalPrice();
    }
}
