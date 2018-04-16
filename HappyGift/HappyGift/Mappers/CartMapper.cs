using HappyGift.Models;
using HappyGift.Models.CartViewModels;
using System.Linq;

namespace HappyGift.Mappers
{
    public static class CartMapper
    {
        public static CartListViewModel ToCartListViewModel(this Cart cart)
        {
            if (cart == null)
            {
                return new CartListViewModel();
            }
            return new CartListViewModel
            {
                CartId = cart.CartId,
                CartItems = cart.CartServices.Select(cs => cs.ToServiceBaseViewModel()).ToList(),
                TotalPrice = cart.CartServices.Sum(cs => cs.Service.Price)
            };
        }
    }
}
