using Microsoft.AspNetCore.Mvc;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            try
            {
                var result = await _reservationService.CreateReservationAsync(reservation);
                return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(string id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByStoreId(string storeId)
        {
            var reservations = await _reservationService.GetReservationsByStoreIdAsync(storeId);
            return Ok(reservations);
        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByServiceId(string serviceId)
        {
            var reservations = await _reservationService.GetReservationsByServiceIdAsync(serviceId);
            return Ok(reservations);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByDate(DateTime date)
        {
            var reservations = await _reservationService.GetReservationsByDateAsync(date);
            return Ok(reservations);
        }

        [HttpGet("customer/{email}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByCustomerEmail(string email)
        {
            var reservations = await _reservationService.GetReservationsByCustomerEmailAsync(email);
            return Ok(reservations);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _reservationService.UpdateReservationAsync(reservation);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(string id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}