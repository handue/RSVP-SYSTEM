using Microsoft.AspNetCore.Mvc;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using RSVP.Core.Models;
using RSVP.Core.Exceptions;
using System.Text.Json;
using RSVP.Core.Interfaces;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly ICalendarService _calendarService;
        private readonly IEmailService _emailService;

        public ReservationController(IReservationService reservationService, IMapper mapper, IEmailService emailService)
        {
            _reservationService = reservationService;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> CreateReservation([FromBody] CreateReservationDto reservationDto)
        {


            // Console.WriteLine($"예약값 확인: {JsonSerializer.Serialize(reservationDto, new JsonSerializerOptions { WriteIndented = true })}");

            var result = await _reservationService.CreateReservationAsync(reservationDto);




            // Console.WriteLine($"예약이후 반환 값 확인: {JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true })}");

            // CreatedAtAction: HTTP 201 Created 응답을 반환하면서 새로 생성된 리소스의 위치를 Location 헤더에 포함시킵니다.
            // CreatedAtAction: Returns an HTTP 201 Created response and includes the location of the newly created resource in the Location header.
            // nameof(GetReservationById): GetReservationById 메서드의 이름을 문자열로 가져옵니다.
            // nameof(GetReservationById): Gets the name of the GetReservationById method as a string.
            // new { id = result.Id }: 라우트 파라미터로 사용될 값을 지정합니다.
            // new { id = result.Id }: Specifies the value to be used as a route parameter.

            return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, ApiResponse<ReservationResponseDto>.CreateSuccess(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> GetReservationById(int id)
        {
            var responseDto = await _reservationService.GetReservationByIdAsync(id);

            return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByStoreId(string storeId)
        {
            var reservations = await _reservationService.GetReservationsByStoreIdAsync(storeId);

            return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(reservations));
        }

        // [HttpGet("service/{serviceId}")]
        // public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByServiceId(string serviceId)
        // {
        //     var reservations = await _reservationService.GetReservationsByServiceIdAsync(serviceId);

        //     return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(reservations));
        // }


        // [HttpGet("date/{date}")]
        // public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByDate(DateTime date)
        // {
        //     var reservations = await _reservationService.GetReservationsByDateAsync(date);

        //     return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(reservations));
        // }

        // [HttpGet("customer/{email}")]
        // public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponseDto>>>> GetReservationsByCustomerEmail(string email)
        // {
        //     var reservations = await _reservationService.GetReservationsByCustomerEmailAsync(email);

        //     return Ok(ApiResponse<IEnumerable<ReservationResponseDto>>.CreateSuccess(reservations));
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        // {

        //     if (id != reservationDto.Id)
        //     {
        //         throw new ArgumentException("Invalid reservation ID");
        //     }

        //     var result = await _reservationService.UpdateReservationAsync(reservationDto);
        //     var responseDto = _mapper.Map<ReservationResponseDto>(result);
        //     return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(responseDto));
        // }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);

            return NoContent();
        }

        // [HttpPut("{id}/confirm")]
        // public async Task<ActionResult<ApiResponse<ReservationResponseDto>>> ConfirmReservation(int id)
        // {
        //     var reservation = await _reservationService.GetReservationByIdAsync(id);

        //     reservation.Status = ReservationStatus.Confirmed.ToString();
        //     var confirmedReservation = await _reservationService.ConfirmReservationAsync(id);

        //     return Ok(ApiResponse<ReservationResponseDto>.CreateSuccess(confirmedReservation));
        // }

    }
}