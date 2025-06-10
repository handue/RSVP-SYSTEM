using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using RSVP.Core.Interfaces;
using System.Collections.Generic;

namespace RSVP.Infrastructure.Services
{
    public class GoogleCalendarService : ICalendarService
    {
        private readonly GoogleAuthService _googleAuthService;
        private readonly IEmailService _emailService;

        public GoogleCalendarService(GoogleAuthService googleAuthService, IEmailService emailService)
        {
            _googleAuthService = googleAuthService;
            _emailService = emailService;
        }

        public async Task<CalendarEventResult> CreateCalendarEventAsync(CreateReservationDto calendarEvent)
        {
            var calendarService = await _googleAuthService.GetCalendarServiceAsync();

            var eventDetails = CreateEventDetails(calendarEvent);

            var request = calendarService.Events.Insert(eventDetails, "primary");

            request.SendUpdates = EventsResource.InsertRequest.SendUpdatesEnum.All;

            var createEvent = await request.ExecuteAsync();

            Console.WriteLine("Event created: " + createEvent.HtmlLink);

            return new CalendarEventResult
            {
                EventId = createEvent.Id,
                HtmlLink = createEvent.HtmlLink
            };
        }

        private Event CreateEventDetails(CreateReservationDto reservation)
        {
            var storeTimeZones = new Dictionary<string, string>
            {
                {"store-1", "America/Los_Angeles"},
                {"store-2", "America/Chicago"},
                {"store-3", "America/New_York"},
                {"Los Angeles", "America/Los_Angeles"},
                {"Texas", "America/Chicago"},
                {"New York", "America/New_York"}
            };


            // * Dictionary throw error if key is not found automatically
            var timezone = storeTimeZones.ContainsKey(reservation.StoreId)
                ? storeTimeZones[reservation.StoreId]
                : storeTimeZones[reservation.StoreName ?? throw new InvalidOperationException("Invalid store ID or name")];

            var startDate = reservation.ReservationDate.ToString("yyyy-MM-dd");
            var startTime = reservation.ReservationTime.ToString(@"hh\:mm\:ss");
            var startDateTime = DateTime.Parse($"{startDate} {startTime}");

            var endDateTime = startDateTime.AddMinutes(30);

            var attendees = new List<EventAttendee>{
                new EventAttendee{ Email = reservation.CustomerEmail}
            };

            // ! not used at the moment cause data is dummy
            // var storeEmails = GetStoreEmails(reservation.StoreEmail);

            // foreach (var email in storeEmails){
            //     attendees.Add(new EventAttendee{
            //         Email = email,

            //     });
            // }

            return new Event
            {
                Summary = $"Reservation: {reservation.CustomerName} at {reservation.StoreName}",
                Description = $"Reservation for {reservation.ServiceName} at {reservation.StoreName} \n Comments: {reservation.Notes}",
                Start = new EventDateTime
                {
                    DateTime = startDateTime,
                    TimeZone = timezone
                },
                End = new EventDateTime
                {
                    DateTime = endDateTime,
                    TimeZone = timezone
                },
                Attendees = attendees,
                Reminders = new Event.RemindersData
                {
                    UseDefault = false,
                    Overrides = new List<EventReminder>{
                        new EventReminder{
                            Method = "email",
                            Minutes = 24 * 60
                        },
                        new EventReminder{
                            Method = "popup",
                            Minutes = 10
                        }
                    }
                }


            };

        }


        public async Task<bool> DeleteCalendarEventAsync(string eventId)
        {
            var calendarService = await _googleAuthService.GetCalendarServiceAsync();

            var request = calendarService.Events.Delete("primary", eventId);
            request.SendUpdates = EventsResource.DeleteRequest.SendUpdatesEnum.All;

            await request.ExecuteAsync();

            Console.WriteLine("Google CalendarEvent deleted: " + eventId);

            return true;
        }



        // ! store email are not used at the moment
        // private string[] GetStoreEmails(string store)
        // {
        //     var baseEmails = new[] { "store1@gmail.com", "store2@gmail.com", "store3@gmail.com" };
        //     if (store == "Los Angeles")
        //     {
        //         return new[] { "store1@gmail.com", "store2@gmail.com" };
        //     }
        //     else if (store == "Texas")
        //     {
        //         return new[] { "store2@gmail.com", "store3@gmail.com" };
        //     }
        //     else if (store == "New York")
        //     {
        //         return new[] { "store1@gmail.com", "store3@gmail.com" };
        //     }
        //     return baseEmails;
        // }
    }


}