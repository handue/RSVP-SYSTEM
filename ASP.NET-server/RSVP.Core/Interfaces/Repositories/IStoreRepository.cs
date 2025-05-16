using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Repositories;

public interface IStoreRepository : IRepository<Store>
{
    Task<Store?> GetByStoreIdAsync(string storeId);
    Task<IEnumerable<Store>> GetStoresByLocationAsync(string location);
    Task<IEnumerable<Store>> GetStoresWithServicesAsync();
    Task<IEnumerable<Store>> GetStoresWithHoursAsync();
    Task<bool> ExistsByStoreIdAsync(string storeId);
} 