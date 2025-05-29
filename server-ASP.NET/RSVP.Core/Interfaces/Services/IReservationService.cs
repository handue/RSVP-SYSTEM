using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IReservationService
    {
        Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto reservationDto);


        Task<ReservationResponseDto?> GetReservationByIdAsync(int id);

        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<IEnumerable<ReservationResponseDto>> GetReservationsByStoreIdAsync(string storeId);
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<IEnumerable<ReservationResponseDto>> GetReservationsByServiceIdAsync(string serviceId);

        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<IEnumerable<ReservationResponseDto>> GetReservationsByDateAsync(DateTime date);

        // * It's only for backend testing.
        // * Not Implemented into frontend 

        Task<IEnumerable<ReservationResponseDto>> GetReservationsByCustomerEmailAsync(string email);


        
        Task<ReservationResponseDto> UpdateReservationAsync(UpdateReservationDto reservationDto);
        Task<ReservationResponseDto> ConfirmReservationAsync(int id);
        Task<bool> DeleteReservationAsync(int id);
        // Task<bool> IsTimeSlotAvailableAsync(string storeId, string serviceId, DateTime date, TimeSpan time);
    }
}