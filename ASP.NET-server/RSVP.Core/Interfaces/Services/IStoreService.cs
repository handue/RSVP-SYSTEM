using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IStoreService
    {
        Task<Store> CreateStoreAsync(Store store);
        Task<Store?> GetStoreByIdAsync(string id);
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<IEnumerable<Store>> GetStoresByLocationAsync(string location);
        Task<Store> UpdateStoreAsync(Store store);
        Task<bool> DeleteStoreAsync(string id);
        Task<bool> IsStoreOpenAsync(string storeId, DateTime date, TimeSpan time);
        // Task<IEnumerable<TimeSpan>> GetAvailableTimeSlotsAsync(string storeId, string serviceId, DateTime date);
    }
} 