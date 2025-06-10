

namespace RSVP.Core.Interfaces
{
    public interface ICalendarService
    {
        Task<CalendarEventResult> CreateCalendarEventAsync(CreateReservationDto calendarEvent);

        Task<bool> DeleteCalendarEventAsync(string eventId);
 
    }
}