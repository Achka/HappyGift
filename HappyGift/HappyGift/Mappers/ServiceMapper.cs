using HappyGift.Models;
using HappyGift.Models.ServiceViewModels;
using System;

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
            if(cartServices.Service == null)
            {
                throw new NullReferenceException("Null reference exception occured in mapper class, please make sure you are eager loading the dependency.");
            }
            return new ServiceBaseViewModel
            {
                ServiceId = cartServices.CartServiceId,
                ServiceDescription = cartServices.Service.Description,
                ServiceImageURL = cartServices.Service.ImageUrl,
                ServicePrice = cartServices.Service.Price
            };
        }

    }
}
