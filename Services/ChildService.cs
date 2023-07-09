using GiftFormAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftFormAPI.Services
{
    public class ChildService : IChildService
    {
        private readonly GiftServiceDbContext _dbContext;

        public ChildService(GiftServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Child>> GetAllChildsAsync()
        {
            return await _dbContext.Children.ToListAsync();
        }

        public async Task<Child?> GetChild(int id)
        {
            return await _dbContext.Children.FindAsync(id)!;
        }

        public async Task CreateChild(Child child)
        {
            _dbContext.Children.Add(child);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateChild(Child child)
        {
            _dbContext.Entry(child).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChild(int id)
        {
            var child = await _dbContext.Children.FindAsync(id);
            if (child != null)
            {
                _dbContext.Children.Remove(child);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Child>> GetAllChildren()
        {
            return await _dbContext.Children.ToListAsync();
        }
    }
}