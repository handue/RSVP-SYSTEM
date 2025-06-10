using System.Threading.Channels;

namespace RSVP.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string htmlContent);
        Task<bool> SendEmailAsync(string[] recipients, string subject, string htmlContent);
        Task<bool> SendBookingConfirmationAsync(ReservationResponseDto reservation, string calendarLink);

        Task<bool> SendBookingCancellationAsync(ReservationResponseDto reservation);
        
        // Task<bool> SendBookingConfirmationToStoreAsync(ReservationResponseDto reservation, string calendarLink, string storeEmail);
        // string CreateEmailMessage(string[] recipients, string subject, string htmlContent);

    }
}