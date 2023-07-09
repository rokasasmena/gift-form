using GiftFormAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftFormAPI.Services
{
    public class GiftService : IGiftService
    {
        private readonly GiftServiceDbContext _dbContext;

        public GiftService(GiftServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Gift>> GetAllGifts()
        {
            return await _dbContext.Gifts.ToListAsync();
        }

        public async Task<Gift> GetGiftById(int id)
        {
            return await _dbContext.Gifts.FindAsync(id);
        }

		public async Task<IList<Gift>> GetGiftsForChild(int childId)
		{
			return await _dbContext.Gifts.Where(a => a.ChildId == childId).ToListAsync();
		}

		public async Task CreateGift(Gift gift)
        {
            _dbContext.Gifts.Add(gift);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateGift(Gift gift)
        {
            _dbContext.Entry(gift).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGift(int id)
        {
            var gift = await _dbContext.Gifts.FindAsync(id);
            if (gift != null)
            {
                _dbContext.Gifts.Remove(gift);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}