using Microsoft.AspNetCore.Mvc;
using RSVP.Core.DTOs;
using RSVP.Core.Models;
using RSVP.Core.Interfaces.Services;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using RSVP.Core.Exceptions;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;

        public StoreController(IStoreService storeService, IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<StoreResponseDto>> CreateStore([FromBody] CreateStoreDto dto)
        {
            var store = _mapper.Map<Store>(dto);
            var result = await _storeService.CreateStoreAsync(store);
            var responseDto = _mapper.Map<StoreResponseDto>(result);
            return CreatedAtAction(nameof(GetStoreById), new { id = result.StoreId }, ApiResponse<StoreResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreResponseDto>> GetStoreById(string id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
                return NotFound(ApiResponse<StoreResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Store not found"
                }));
            var responseDto = _mapper.Map<StoreResponseDto>(store);
            return Ok(ApiResponse<StoreResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreResponseDto>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            var responseDtos = _mapper.Map<IEnumerable<StoreResponseDto>>(stores);
            return Ok(ApiResponse<IEnumerable<StoreResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<StoreResponseDto>>> GetStoresByLocation(string location)
        {
            var stores = await _storeService.GetStoresByLocationAsync(location);
            var responseDtos = _mapper.Map<IEnumerable<StoreResponseDto>>(stores);
            return Ok(ApiResponse<IEnumerable<StoreResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StoreResponseDto>> UpdateStore(string id, [FromBody] CreateStoreDto dto)
        {
            var store = _mapper.Map<Store>(dto);
            store.StoreId = id;
            var result = await _storeService.UpdateStoreAsync(store);
            var responseDto = _mapper.Map<StoreResponseDto>(result);
            return Ok(ApiResponse<StoreResponseDto>.CreateSuccess(responseDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(string id)
        {
            var result = await _storeService.DeleteStoreAsync(id);
            if (!result)
                return NotFound(ApiResponse<StoreResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Store not found"
                }));
            return NoContent();
        }

        // ! Not Used at the moment
        // [HttpGet("{storeId}/is-open")]
        // public async Task<ActionResult<bool>> IsStoreOpen(string storeId, [FromQuery] DateTime date, [FromQuery] TimeSpan time)
        // {
        //     var isOpen = await _storeService.IsStoreOpenAsync(storeId, date, time);
        //     return Ok(isOpen);
        // }

        // [HttpGet("{storeId}/available-slots")]
        // public async Task<ActionResult<IEnumerable<TimeSpan>>> GetAvailableTimeSlots(
        //     string storeId, [FromQuery] string serviceId, [FromQuery] DateTime date)
        // {
        //     try
        //     {
        //         var timeSlots = await _storeService.GetAvailableTimeSlotsAsync(storeId, serviceId, date);
        //         return Ok(timeSlots);
        //     }
        //     catch (ArgumentException ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}