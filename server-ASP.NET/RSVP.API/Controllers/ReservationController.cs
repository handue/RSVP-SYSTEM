using Microsoft.AspNetCore.Mvc;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using RSVP.Core.Models;
using RSVP.Core.Exceptions;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> CreateReservation([FromBody] CreateReservationDto dto)
        {   
            
            Console.WriteLine($"[Controller] CreateReservation: {dto}");
            var reservation = _mapper.Map<Reservation>(dto);
            var result = await _reservationService.CreateReservationAsync(reservation);
            var responseDto = _mapper.Map<ReservationResponseDto>(result);
            return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found"
                }));
            var responseDto = _mapper.Map<ReservationResponseDto>(reservation);
            return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByStoreId(string storeId)
        {
            var reservations = await _reservationService.GetReservationsByStoreIdAsync(storeId);
            var responseDtos = _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
            return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByServiceId(string serviceId)
        {
            var reservations = await _reservationService.GetReservationsByServiceIdAsync(serviceId);
            var responseDtos = _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
            return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByDate(DateTime date)
        {
            var reservations = await _reservationService.GetReservationsByDateAsync(date);
            var responseDtos = _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
            return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpGet("customer/{email}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByCustomerEmail(string email)
        {
            var reservations = await _reservationService.GetReservationsByCustomerEmailAsync(email);
            var responseDtos = _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
            return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> UpdateReservation(int id, [FromBody] CreateReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            reservation.Id = id;
            var result = await _reservationService.UpdateReservationAsync(reservation);
            var responseDto = _mapper.Map<ReservationResponseDto>(result);
            return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (!result)
                return NotFound(ApiResponse<ReservationResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found"
                }));
            return NoContent();
        }

        [HttpPut("{id}/confirm")]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> ConfirmReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found"
                }));
            reservation.Status = ReservationStatus.Confirmed;
            var result = await _reservationService.UpdateReservationAsync(reservation);
            var responseDto = _mapper.Map<ReservationResponseDto>(result);
            return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        }

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> CancelReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found"
                }));
            reservation.Status = ReservationStatus.Cancelled;
            var result = await _reservationService.UpdateReservationAsync(reservation);
            var responseDto = _mapper.Map<ReservationResponseDto>(result);
            return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        }
    }
}