using HappyGift.Models;

namespace HappyGift.Managers.Interfaces
{
    public interface ICartManager 
    {
        Cart CreateNewCart(string userId);
        bool AddServiceToCart(int serviceId, int cartId);
        Cart GetCartByUserId(string userId);
        void RemoveFromCart(int cartServiceId, string userId);
    }
}
