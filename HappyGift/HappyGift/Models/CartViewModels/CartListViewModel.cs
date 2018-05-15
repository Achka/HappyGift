using HappyGift.Models.ServiceViewModels;
using System.Collections.Generic;

namespace HappyGift.Models.CartViewModels
{
    public class CartListViewModel
    {
        public int CartId { get; set; }
        public string City { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ServiceBaseViewModel> CartItems { get; set; }
    }
}
