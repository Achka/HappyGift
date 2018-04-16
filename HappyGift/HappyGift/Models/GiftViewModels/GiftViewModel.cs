using HappyGift.Models.ServiceViewModels;
using System;
using System.Collections.Generic;

namespace HappyGift.Models.GiftViewModels
{
    public class GiftViewModel
    {
        public string UserEmail { get; set; }
        public long GiftId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsAcceptedByAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ServiceBaseViewModel> Services { get; set; }
        public DateTime ExpirationDate { get; internal set; }
    }
}
