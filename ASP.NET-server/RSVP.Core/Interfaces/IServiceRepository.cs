using RSVP.Core.Models;

namespace RSVP.Core.Interfaces;

public interface IServiceRepository : IRepository<Service>
{
    Task<Service?> GetByServiceIdAsync(string serviceId);
    Task<IEnumerable<Service>> GetServicesByStoreIdAsync(string storeId);
    Task<IEnumerable<Service>> GetServicesWithReservationsAsync();
    Task<bool> ExistsByServiceIdAsync(string serviceId);
} 