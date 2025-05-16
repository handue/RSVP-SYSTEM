using RSVP.Core.Models;

namespace RSVP.Core.Interfaces;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetReservationsByStoreIdAsync(string storeId);
    Task<IEnumerable<Reservation>> GetReservationsByServiceIdAsync(string serviceId);
    Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);
    Task<IEnumerable<Reservation>> GetReservationsByCustomerEmailAsync(string email);
    Task<IEnumerable<Reservation>> GetReservationsWithDetailsAsync();
    Task<bool> IsTimeSlotAvailableAsync(string storeId, string serviceId, DateTime date, TimeSpan time);
} 