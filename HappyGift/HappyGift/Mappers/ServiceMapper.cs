using HappyGift.Models;
using HappyGift.Models.ServiceViewModels;
using System;
using System.Linq;

namespace HappyGift.Mappers
{
    public static class ServiceMapper
    {
        public static ServiceBaseViewModel ToServiceBaseViewModel(this Service service)
        {
            return new ServiceBaseViewModel
            {
                ServiceId = service.Id,
                ServiceDescription = service.Description,
                ServiceImageURL = service.ImageUrl,
                ServicePrice = service.Price,
                ServiceName = service.Name
            };
        }

        public static ServiceBaseViewModel ToServiceBaseViewModel(this CartServices cartServices)
        {
            if(cartServices?.Service == null)
            {
                throw new NullReferenceException("Null reference exception occured in mapper class, please make sure you are eager loading the dependency.");
            }
            return new ServiceBaseViewModel
            {
                ServiceId = cartServices.CartServiceId,
                ServiceDescription = cartServices.Service.Description,
                ServiceImageURL = cartServices.Service.ImageUrl,
                ServicePrice = cartServices.Service.Price,
                AvarageAgeOfCustomer = Convert.ToInt32(cartServices.Service.GiftServices.Any() ?
                    cartServices.Service.GiftServices.Average(gs => (double)(DateTime.Now.Year - gs.Gift.User?.YearOfBirth.GetValueOrDefault(1900)??0)) : 0),
                MinAgeOfUser = cartServices.Service.GiftServices.Any() ?
                    cartServices.Service.GiftServices.Min(gs => DateTime.Now.Year - gs.Gift?.User.YearOfBirth.GetValueOrDefault(1900)?? 0) : 0,
                MaxAgeOfUser = cartServices.Service.GiftServices.Any() ?
                cartServices.Service.GiftServices.Max(gs => DateTime.Now.Year - gs.Gift?.User.YearOfBirth.GetValueOrDefault(1900)??0) :0
            };
        }

    }
}
