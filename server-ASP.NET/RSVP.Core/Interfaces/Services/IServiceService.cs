using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IServiceService
    {
        // * It's only for backend testing.
        // * Not Implemented into frontend 

        Task<ServiceResponseDto> CreateServiceAsync(CreateServiceDto serviceDto);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<ServiceResponseDto> GetServiceByIdAsync(string id);
        Task<IEnumerable<ServiceResponseDto>> GetServicesByStoreIdAsync(string storeId);
        Task<IEnumerable<ServiceResponseDto>> GetAllServicesAsync();

        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<ServiceResponseDto> UpdateServiceAsync(CreateServiceDto service);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<bool> DeleteServiceAsync(string id);
        // Task<bool> IsServiceAvailableAsync(string serviceId, DateTime date, TimeSpan time);
    }
}