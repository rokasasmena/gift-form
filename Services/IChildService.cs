using GiftFormAPI.Models;

namespace GiftFormAPI.Services
{
    public interface IChildService
    {
        Task<List<Child>> GetAllChildsAsync();
        Task<Child?> GetChild(int id);
        Task CreateChild(Child child);
        Task UpdateChild(Child child);
        Task DeleteChild(int id);
        Task<List<Child>> GetAllChildren();
    }
}