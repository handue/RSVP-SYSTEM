using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IServiceService
    {
        Task<ServiceResponseDto> CreateServiceAsync(CreateServiceDto serviceDto);
        Task<ServiceResponseDto> GetServiceByIdAsync(string id);
        Task<IEnumerable<ServiceResponseDto>> GetServicesByStoreIdAsync(string storeId);
        Task<IEnumerable<ServiceResponseDto>> GetAllServicesAsync();
        Task<ServiceResponseDto> UpdateServiceAsync(CreateServiceDto service);
        Task<bool> DeleteServiceAsync(string id);
        // Task<bool> IsServiceAvailableAsync(string serviceId, DateTime date, TimeSpan time);
    }
} 