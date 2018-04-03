using HappyGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Managers.Interfaces
{
    public interface ICartManager 
    {
        bool CreateNewCart(string userId);
        bool AddServiceToCart(int serviceId, int cartId);
        Cart GetCartByUserId(string userId);
    }
}
