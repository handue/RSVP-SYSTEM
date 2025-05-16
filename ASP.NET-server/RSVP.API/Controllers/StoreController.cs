using Microsoft.AspNetCore.Mvc;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<ActionResult<Store>> CreateStore(Store store)
        {
            try
            {
                var result = await _storeService.CreateStoreAsync(store);
                return CreatedAtAction(nameof(GetStoreById), new { id = result.StoreId }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStoreById(string id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
                return NotFound();

            return Ok(store);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoresByLocation(string location)
        {
            var stores = await _storeService.GetStoresByLocationAsync(location);
            return Ok(stores);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Store>> UpdateStore(string id, Store store)
        {
            if (id != store.StoreId)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _storeService.UpdateStoreAsync(store);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(string id)
        {
            var result = await _storeService.DeleteStoreAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{storeId}/is-open")]
        public async Task<ActionResult<bool>> IsStoreOpen(string storeId, [FromQuery] DateTime date, [FromQuery] TimeSpan time)
        {
            var isOpen = await _storeService.IsStoreOpenAsync(storeId, date, time);
            return Ok(isOpen);
        }

        [HttpGet("{storeId}/available-slots")]
        public async Task<ActionResult<IEnumerable<TimeSpan>>> GetAvailableTimeSlots(
            string storeId, [FromQuery] string serviceId, [FromQuery] DateTime date)
        {
            try
            {
                var timeSlots = await _storeService.GetAvailableTimeSlotsAsync(storeId, serviceId, date);
                return Ok(timeSlots);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 