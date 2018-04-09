using HappyGift.Models;
using HappyGift.Models.GiftViewModels;
using System.Linq;

namespace HappyGift.Mappers
{
    public static class GiftMapper
    {
        public static GiftViewModel ToGiftViewModel(this Gift gift)
        {
            return new GiftViewModel
            {
                GiftId = gift.Id,
                CreatedDate = gift.CreatedDate,
                UserEmail = gift.User.UserName,
                IsAcceptedByAdmin = gift.IsAcceptedByAdmin,
                Services = gift.GiftServices.Select(gs => gs.Service.ToServiceBaseViewModel()).ToList(),
                TotalPrice = gift.GiftServices.Sum(gs => gs.Service.Price),
            };
        }
    }
}
