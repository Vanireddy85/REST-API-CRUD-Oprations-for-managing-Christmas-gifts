using ChristmasWishList.Shared.Requests;
using ChristmasWishList.Shared.Responses;

namespace ChristmasWishList.BusinessLayer.Services.Interfaces
{
    public interface IGiftService
    {
        GiftResponse Get();
        Gift GetGift(Guid id);
        Gift CreateGift(SaveGiftRequest request);
        Gift UpdateGifit(Guid id,SaveGiftRequest request);
        void DeleteGift(Guid id);
        void DeleteGiftList();

    }
}
