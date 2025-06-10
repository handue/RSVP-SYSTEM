using System.Security;
using System.Text;
using Google.Apis.Gmail.v1.Data;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;

namespace RSVP.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly GoogleAuthService _googleAuthService;
        private readonly IConfiguration _configuration;


        public EmailService(GoogleAuthService googleAuthService, IConfiguration configuration)
        {
            _googleAuthService = googleAuthService;
            _configuration = configuration;

        }

        public async Task<bool> SendEmailAsync(string to, string subject, string htmlContent)
        {
            return await SendEmailAsync(new[] { to }, subject, htmlContent);
        }

        public async Task<bool> SendEmailAsync(string[] recipients, string subject, string htmlContent)
        {

            var gmailService = await _googleAuthService.GetGmailServiceAsync();

            var message = CreateEmailMessage(recipients, subject, htmlContent);
            var gmailMessage = new Message
            {
                Raw = Base64UrlEncode(message)
            };

            var result = gmailService.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();

            Console.WriteLine($"Email Sent successfully. Message ID : {result.Id}");

            return true;
        }

        public async Task<bool> SendBookingConfirmationAsync(ReservationResponseDto reservation, string calendarLink)
        {
            var htmlContent = GenerateBookingConfirmationHtml(reservation, calendarLink);
            // todo: If it's production environment, should send email to store either. but it's fake now so it's not needed.
            return await SendEmailAsync(reservation.CustomerEmail, "Reservation Confirmation", htmlContent);
        }

        // todo: store email is dummy data now, don't use it at the moment. 
        // public async Task<bool> SendBookingConfirmationToStoreAsync(ReservationResponseDto reservation, string calendarLink, string storeEmail)
        // {
        //     return await Task.FromResult(true);
        // }


        public async Task<bool> SendBookingCancellationAsync(ReservationResponseDto reservation)
        {
            var htmlContent = GenerateBookingCancellationHtml(reservation);
            return await SendEmailAsync(reservation.CustomerEmail, "Reservation Cancellation", htmlContent);
        }

        private string CreateEmailMessage(string[] recipients, string subject, string htmlContent)
        {

            // * Gmail api also provides a way to send emails, but it's fit for simple emails
            // * that's why we're using MimeKit to create a more complex email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RSVP System", _configuration["EmailAddress"]));

            foreach (var recipient in recipients)
            {
                message.To.Add(new MailboxAddress("", recipient));
            }

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlContent
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var stream = new MemoryStream();
            message.WriteTo(stream);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }


        private string GenerateBookingConfirmationHtml(ReservationResponseDto reservation, string calendarLink)
        {
            var modifyLink = $"{_configuration["Kestrel:Endpoints:Http:Url"]}/reservation/{reservation.Id}";

            return $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                    <h2 style='color: #333; text-align: center; margin-bottom: 30px;'>Reservation Confirmation</h2>
                    
                    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 6px; margin-bottom: 20px;'>
                        <p style='margin: 10px 0;'><strong>Store:</strong> {reservation.StoreName}</p>
                        <p style='margin: 10px 0;'><strong>Service:</strong> {reservation.ServiceName}</p>
                        <p style='margin: 10px 0;'><strong>Date:</strong> {reservation.ReservationDate:MMM dd, yyyy}</p>
                        <p style='margin: 10px 0;'><strong>Time:</strong> {reservation.ReservationTime:hh\:mm}</p>
                    </div>

                    <div style='text-align: center; margin: 30px 0;'>
                        <a href='{modifyLink}' style='background-color: #007bff; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; display: inline-block; margin-right: 10px;'>
                            Modify Reservation
                        </a>
                        <a href='{calendarLink}' style='background-color: #4285f4; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                            Add to Google Calendar
                        </a>
                    </div>

                    <div style='text-align: center; color: #666; font-size: 14px;'>
                        <p>If you have any questions, please contact our customer service.</p>
                    </div>
                </div>";
        }

        private string GenerateBookingCancellationHtml(ReservationResponseDto reservation)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;'>
                    <h2 style='color: #333; text-align: center; margin-bottom: 30px;'>Reservation Cancellation Confirmation</h2>
                    
                    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 6px; margin-bottom: 20px;'>
                        <p style='margin: 10px 0;'><strong>Store:</strong> {reservation.StoreName}</p>
                        <p style='margin: 10px 0;'><strong>Service:</strong> {reservation.ServiceName}</p>
                        <p style='margin: 10px 0;'><strong>Date:</strong> {reservation.ReservationDate:MMM dd, yyyy}</p>
                        <p style='margin: 10px 0;'><strong>Time:</strong> {reservation.ReservationTime:hh\:mm}</p>
                        <p style='margin: 10px 0;'><strong>Customer Name:</strong> {reservation.CustomerName}</p>
                    </div>

                    <div style='text-align: center; margin: 30px 0;'>
                        <p style='color: #dc3545; font-weight: bold;'>Your reservation has been successfully cancelled.</p>
                    </div>

                    <div style='text-align: center; color: #666; font-size: 14px;'>
                        <p>If you have any questions, please contact our customer service.</p>
                    </div>
                </div>";
        }
    }
}