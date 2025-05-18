using Microsoft.AspNetCore.Mvc;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RSVP.API.Middleware;
using RSVP.Core.Exceptions;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(
            IReservationService reservationService,
            IStoreRepository storeRepository,
            IServiceRepository serviceRepository,
            IReservationRepository reservationRepository)
        {
            _reservationService = reservationService;
            _storeRepository = storeRepository;
            _serviceRepository = serviceRepository;
            _reservationRepository = reservationRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Reservation>>> CreateReservation(Reservation reservation)
        {
            var result = await _reservationService.CreateReservationAsync(reservation);
            return CreatedAtAction(
                // first parameter: action name
                // meaning = name of the action to call, in this case, it means where you can find this data value throw the route. 
                // * 즉, 내가 현재 반환하는 값을 어디서 확인할수 있는지를 알려주는 절차인데, 첫 번째 매개변수에서는 확인될 메소드(컨트롤러)의 이름을 ASP.NET에 알려줘서 자동으로 찾게 만듬. (이름 불일치하면 오류남) / 정확히는 어떤 컨트롤러 액션으로 url 을 생성할지 알려주는것.
                // * 이후 매개변수가 있으면 그 매개변수를 찾아서 넣어주고, 없으면 안넣어주는데, 한 번에 생성하는게 아닌이상, 보통은 매개변수가 있음.
                // * first & second parameter will be used to generate the url of the response body(especially in Header as a 'Location')

                nameof(GetReservationById),
                // second parameter: route values
                // meaning = values to be used for the route. 
                new { id = result.Id },
                // third parameter: response body
                ApiResponse<Reservation>.CreateSuccess(result)
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Reservation>>> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<Reservation>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found",
                    Details = null
                }));

            return Ok(ApiResponse<Reservation>.CreateSuccess(reservation));
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Reservation>>>> GetReservationsByStoreId(string storeId)
        {
            var reservations = await _reservationService.GetReservationsByStoreIdAsync(storeId);
            if (reservations == null)
                return NotFound(ApiResponse<IEnumerable<Reservation>>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "No reservations found for the store",
                    Details = null
                }));

            return Ok(ApiResponse<IEnumerable<Reservation>>.CreateSuccess(reservations));
        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Reservation>>>> GetReservationsByServiceId(string serviceId)
        {
            var reservations = await _reservationService.GetReservationsByServiceIdAsync(serviceId);
            if (reservations == null)
                return NotFound(ApiResponse<IEnumerable<Reservation>>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "No reservations found for the service",
                    Details = null
                }));

            return Ok(ApiResponse<IEnumerable<Reservation>>.CreateSuccess(reservations));
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Reservation>>>> GetReservationsByDate(DateTime date)
        {
            var reservations = await _reservationService.GetReservationsByDateAsync(date);
            if (reservations == null)
                return NotFound(ApiResponse<IEnumerable<Reservation>>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "No reservations found for the given date",
                    Details = null
                }));

            return Ok(ApiResponse<IEnumerable<Reservation>>.CreateSuccess(reservations));
        }

        [HttpGet("customer/{email}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Reservation>>>> GetReservationsByCustomerEmail(string email)
        {
            var reservations = await _reservationService.GetReservationsByCustomerEmailAsync(email);
            if (reservations == null)
                return NotFound(ApiResponse<IEnumerable<Reservation>>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "No reservations found for the given customer email",
                    Details = null
                }));

            return Ok(ApiResponse<IEnumerable<Reservation>>.CreateSuccess(reservations));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Reservation>>> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest(ApiResponse<Reservation>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.ValidationError,
                    Message = "ID mismatch",
                    Details = null
                }));
            }

            var result = await _reservationService.UpdateReservationAsync(reservation);
            return Ok(ApiResponse<Reservation>.CreateSuccess(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (!result)
                return NotFound(ApiResponse<bool>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found",
                    Details = null
                }));

            return NoContent();
        }

        [HttpPut("{id}/confirm")]
        public async Task<ActionResult<ApiResponse<Reservation>>> ConfirmReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<Reservation>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found",
                    Details = null
                }));

            reservation.Status = ReservationStatus.Confirmed;
            var result = await _reservationService.UpdateReservationAsync(reservation);
            return Ok(ApiResponse<Reservation>.CreateSuccess(result));
        }

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<ApiResponse<Reservation>>> CancelReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<Reservation>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Reservation not found",
                    Details = null
                }));

            reservation.Status = ReservationStatus.Cancelled;
            var result = await _reservationService.UpdateReservationAsync(reservation);
            return Ok(ApiResponse<Reservation>.CreateSuccess(result));
        }

        // ! not used at the moment. 
        // [HttpPost("calendar")]
        // public async Task<ActionResult<IEnumerable<TimeSpan>>> GetAvailableTimeSlots([FromBody] Reservation reservation)
        // {

        //     var date = reservation.ReservationDate;
        //     var storeId = reservation.StoreId;
        //     var serviceId = reservation.ServiceId;

        //     // 1. 매장의 영업 시간 확인
        //     var store = await _storeRepository.GetByStoreIdAsync(storeId);
        //     if (store == null)
        //         return NotFound("Store not found");

        //     // 2. 서비스의 소요 시간 확인
        //     var service = await _serviceRepository.GetByServiceIdAsync(serviceId);
        //     if (service == null)
        //         return NotFound("Service not found");

        //     // 3. 해당 날짜의 예약 목록 조회
        //     var reservations = await _reservationRepository.GetReservationsByDateAsync(date);

        //     // 4. 예약 가능한 시간 슬롯 계산
        //     var availableTimeSlots = new List<TimeSpan>();
        //     var startTime = new TimeSpan(9, 0, 0); // 오전 9시
        //     var endTime = new TimeSpan(18, 0, 0);  // 오후 6시
        //     var interval = new TimeSpan(0, 30, 0); // 30분 간격

        //     for (var time = startTime; time <= endTime; time = time.Add(interval))
        //     {
        //         var isAvailable = true;
        //         var slotEndTime = time.Add(TimeSpan.FromMinutes(service.Duration));

        //         // 해당 시간대에 예약이 있는지 확인
        //         foreach (var existingReservation in reservations)
        //         {
        //             if (existingReservation.Status == ReservationStatus.Cancelled)
        //                 continue;

        //             var reservationEndTime = existingReservation.ReservationTime.Add(TimeSpan.FromMinutes(service.Duration));
        //             if ((time >= existingReservation.ReservationTime && time < reservationEndTime) ||
        //                 (slotEndTime > existingReservation.ReservationTime && slotEndTime <= reservationEndTime))
        //             {
        //                 isAvailable = false;
        //                 break;
        //             }
        //         }

        //         if (isAvailable)
        //             availableTimeSlots.Add(time);
        //     }

        //     return Ok(availableTimeSlots);
        // }


    }
}