using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IServiceService
    {
        Task<Service> CreateServiceAsync(Service service);
        Task<Service> GetServiceByIdAsync(string id);
        Task<IEnumerable<Service>> GetServicesByStoreIdAsync(string storeId);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> UpdateServiceAsync(Service service);
        Task<bool> DeleteServiceAsync(string id);
        // Task<bool> IsServiceAvailableAsync(string serviceId, DateTime date, TimeSpan time);
    }
} 