using Microsoft.AspNetCore.Mvc;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        public async Task<ActionResult<Service>> CreateService(Service service)
        {
            try
            {
                var result = await _serviceService.CreateServiceAsync(service);
                return CreatedAtAction(nameof(GetServiceById), new { id = result.ServiceId }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetServiceById(string id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServicesByStoreId(string storeId)
        {
            var services = await _serviceService.GetServicesByStoreIdAsync(storeId);
            return Ok(services);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> UpdateService(string id, Service service)
        {
            if (id != service.ServiceId)
                return BadRequest("ID mismatch");

            try
            {
                var result = await _serviceService.UpdateServiceAsync(service);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(string id)
        {
            var result = await _serviceService.DeleteServiceAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{serviceId}/is-available")]
        public async Task<ActionResult<bool>> IsServiceAvailable(
            string serviceId, [FromQuery] DateTime date, [FromQuery] TimeSpan time)
        {
            var isAvailable = await _serviceService.IsServiceAvailableAsync(serviceId, date, time);
            return Ok(isAvailable);
        }
    }
} 