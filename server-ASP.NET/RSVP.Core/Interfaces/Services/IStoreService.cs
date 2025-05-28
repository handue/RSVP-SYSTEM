using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IStoreService
    {
        Task<StoreResponseDto> CreateStoreAsync(CreateStoreDto createStoreDto);
        Task<StoreResponseDto> GetStoreByIdAsync(string id);
        Task<IEnumerable<StoreResponseDto>> GetAllStoresAsync();
        Task<IEnumerable<StoreResponseDto>> GetStoresByLocationAsync(string location);
        Task<StoreResponseDto> UpdateStoreAsync(UpdateStoreDto updateStoreDto);
        Task<bool> DeleteStoreAsync(string id);
        Task<bool> IsStoreOpenAsync(string storeId, DateTime date, TimeSpan time);
        // Task<IEnumerable<TimeSpan>> GetAvailableTimeSlotsAsync(string storeId, string serviceId, DateTime date);
    }
} 