using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IStoreService
    {
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<StoreResponseDto> CreateStoreAsync(CreateStoreDto createStoreDto);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<StoreResponseDto> GetStoreByIdAsync(string id);


        Task<IEnumerable<StoreResponseDto>> GetAllStoresAsync();

        Task<IEnumerable<StoreResponseDto>> SaveAllStoresAsync(IEnumerable<StoreResponseDto> storeResponseDtos);

        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<IEnumerable<StoreResponseDto>> GetStoresByLocationAsync(string location);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<StoreResponseDto> UpdateStoreAsync(UpdateStoreDto updateStoreDto);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<bool> DeleteStoreAsync(string id);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<bool> IsStoreOpenAsync(string storeId, DateTime date, TimeSpan time);
        // Task<IEnumerable<TimeSpan>> GetAvailableTimeSlotsAsync(string storeId, string serviceId, DateTime date);
    }
}