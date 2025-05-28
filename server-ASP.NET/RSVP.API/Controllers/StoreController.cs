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
        public async Task<ActionResult<StoreResponseDto>> CreateStore([FromBody] CreateStoreDto createStoreDto)
        {

            var result = await _storeService.CreateStoreAsync(createStoreDto);

            return CreatedAtAction(nameof(GetStoreById), new { id = result.StoreId }, ApiResponse<StoreResponseDto>.CreateSuccess(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreResponseDto>> GetStoreById(string id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);

            var responseDto = _mapper.Map<StoreResponseDto>(store);
            return Ok(ApiResponse<StoreResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreResponseDto>>> GetAllStores()
        {
            var storeDtos = await _storeService.GetAllStoresAsync();

            return Ok(ApiResponse<IEnumerable<StoreResponseDto>>.CreateSuccess(storeDtos));
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<StoreResponseDto>>> GetStoresByLocation(string location)
        {
            var storesDto = await _storeService.GetStoresByLocationAsync(location);

            return Ok(ApiResponse<IEnumerable<StoreResponseDto>>.CreateSuccess(storesDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StoreResponseDto>> UpdateStore(string id, [FromBody] UpdateStoreDto updateStoreDto)
        {

            if (updateStoreDto.Id != id)
            {
                throw new ArgumentException("Invalid store ID");
            }

            var result = await _storeService.UpdateStoreAsync(updateStoreDto);
            var responseDto = _mapper.Map<StoreResponseDto>(result);
            return Ok(ApiResponse<StoreResponseDto>.CreateSuccess(responseDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(string id)
        {
            var result = await _storeService.DeleteStoreAsync(id);
        
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