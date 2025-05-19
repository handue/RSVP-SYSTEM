using Microsoft.AspNetCore.Mvc;
using RSVP.API.Middleware;
using RSVP.Core.Exceptions;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;
using RSVP.Core.DTOs;

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

            var result = await _serviceService.CreateServiceAsync(service);
            return CreatedAtAction(nameof(GetServiceById), new { id = result.ServiceId }, ApiResponse<Service>.CreateSuccess(result));


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetServiceById(string id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound(ApiResponse<Service>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Service not found",
                    Details = null,
                }));

            return Ok(ApiResponse<Service>.CreateSuccess(service));
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServicesByStoreId(string storeId)
        {
            var services = await _serviceService.GetServicesByStoreIdAsync(storeId);
            return Ok(ApiResponse<IEnumerable<Service>>.CreateSuccess(services));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(ApiResponse<IEnumerable<Service>>.CreateSuccess(services));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> UpdateService(string id, Service service)
        {
            if (id != service.ServiceId)
                return BadRequest(ApiResponse<Service>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.ValidationError,
                    Message = "ID mismatch",
                    Details = null,
                }));


            var result = await _serviceService.UpdateServiceAsync(service);
            return Ok(ApiResponse<Service>.CreateSuccess(result));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(string id)
        {
            var result = await _serviceService.DeleteServiceAsync(id);
            if (!result)
                return NotFound(ApiResponse<Service>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Service not found",
                    Details = null,
                }));

            return NoContent();
        }

        // ! Not Used at the moment
        // [HttpGet("{serviceId}/is-available")]
        // public async Task<ActionResult<bool>> IsServiceAvailable(
        //     string serviceId, [FromQuery] DateTime date, [FromQuery] TimeSpan time)
        // {
        //     var isAvailable = await _serviceService.IsServiceAvailableAsync(serviceId, date, time);
        //     return Ok(isAvailable);
        // }
    }
}