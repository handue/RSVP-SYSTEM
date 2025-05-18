using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByStoreIdAsync(string storeId);
        Task<IEnumerable<Reservation>> GetReservationsByServiceIdAsync(string serviceId);
        Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<IEnumerable<Reservation>> GetReservationsByCustomerEmailAsync(string email);
        Task<Reservation> UpdateReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
        // Task<bool> IsTimeSlotAvailableAsync(string storeId, string serviceId, DateTime date, TimeSpan time);
    }
} 