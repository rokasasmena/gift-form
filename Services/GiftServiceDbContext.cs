using GiftFormAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftFormAPI.Services
{
    public class GiftServiceDbContext : DbContext
    {
        public GiftServiceDbContext(DbContextOptions<GiftServiceDbContext> options) : base(options) {}
        public DbSet<Child> Children { get; set; }
        public DbSet<Gift> Gifts { get; set; }
    }
}