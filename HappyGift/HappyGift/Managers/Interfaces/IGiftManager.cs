using System;
using HappyGift.Models;
using System.Collections.Generic;

namespace HappyGift.Managers.Interfaces
{
    public interface IGiftManager : IDisposable
    {
        void CreateGiftFromCart(string userId);
        List<Gift> GetNotApprovedGifts();
        void ApproveGift(long giftId);
        List<Gift> GetGiftsByUser(string userId);
    }
}
