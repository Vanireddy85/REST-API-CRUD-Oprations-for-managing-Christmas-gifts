using ChristmasWishList.BusinessLayer.Services.Interfaces;
using ChristmasWishList.Shared.Requests;
using ChristmasWishList.Shared.Responses;

namespace ChristmasWishList.BusinessLayer.Services
{
    public class GiftService : IGiftService
    {
        private static readonly List<Gift> gifts = new();

        
        public GiftResponse Get()
        {

            var listGifts = gifts.ToList();

            var totalAmount = listGifts
                 .Select(a => a.Price)
                 .Sum();

            var totalSpent = listGifts
               .Where(a => a.IsPurchased)
               .Sum(a => a.Price);

            var totalToSpent = listGifts
                .Where(a => !a.IsPurchased)
                .Sum(a => a.Price);

            var totalGift = listGifts.Count;

            return new GiftResponse(totalGift, totalAmount, totalSpent, totalToSpent, listGifts);

        }

        public Gift GetGift(Guid id)
        {
            var gift = gifts.FirstOrDefault(a => a.Id == id);

            if (gift is null)
            {
                return null;
            }
            return gift;
        }

        public Gift CreateGift(SaveGiftRequest request)
        {

            var createGift = new Gift
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                NameGift = request.NameGift,
                Price = request.Price,
                IsPurchased = request.IsPurchased,
                CreatedDate = DateTime.Now
                
            };

            gifts.Add(createGift);

            return createGift;
        }
        
        public void DeleteGift(Guid id)
        {
            var gift = gifts.FirstOrDefault(a => a.Id == id);
            gifts.Remove(gift);
        }

        public Gift UpdateGifit(Guid id, SaveGiftRequest request)
        {

            var gift = gifts.FirstOrDefault(g => g.Id == id);

            if (gift == null)
            {
                return null;
            }

            gift.FirstName = request.FirstName;
            gift.NameGift = request.NameGift;
            gift.Price = request.Price;
            gift.IsPurchased = request.IsPurchased;
            gift.LastModifiedDate = DateTime.Now;

            return gift;
        }

        public void DeleteGiftList()
        {
            gifts.Clear();
        }

    }
}
