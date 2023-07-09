using GiftFormAPI.Models;

namespace GiftFormAPI.Services
{
    public interface IGiftService
    {
        Task<List<Gift>> GetAllGifts();
        Task<IList<Gift>> GetGiftsForChild(int childId);
		Task<Gift> GetGiftById(int id);
        Task CreateGift(Gift gift);
        Task UpdateGift(Gift gift);
        Task DeleteGift(int id);
    }
}
