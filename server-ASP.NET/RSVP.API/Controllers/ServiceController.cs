using Microsoft.AspNetCore.Mvc;
using RSVP.API.Middleware;
using RSVP.Core.Exceptions;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;
using RSVP.Core.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponseDto>> CreateService([FromBody] CreateServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            var result = await _serviceService.CreateServiceAsync(service);
            var responseDto = _mapper.Map<ServiceResponseDto>(result);
            return CreatedAtAction(nameof(GetServiceById), new { id = result.ServiceId }, ApiResponse<ServiceResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponseDto>> GetServiceById(string id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
                return NotFound(ApiResponse<ServiceResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Service not found"
                }));
            var responseDto = _mapper.Map<ServiceResponseDto>(service);
            return Ok(ApiResponse<ServiceResponseDto>.CreateSuccess(responseDto));
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<ServiceResponseDto>>> GetServicesByStoreId(string storeId)
        {
            var services = await _serviceService.GetServicesByStoreIdAsync(storeId);
            var responseDtos = _mapper.Map<IEnumerable<ServiceResponseDto>>(services);
            return Ok(ApiResponse<IEnumerable<ServiceResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceResponseDto>>> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            var responseDtos = _mapper.Map<IEnumerable<ServiceResponseDto>>(services);
            return Ok(ApiResponse<IEnumerable<ServiceResponseDto>>.CreateSuccess(responseDtos));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponseDto>> UpdateService(string id, [FromBody] CreateServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            service.ServiceId = id;
            var result = await _serviceService.UpdateServiceAsync(service);
            var responseDto = _mapper.Map<ServiceResponseDto>(result);
            return Ok(ApiResponse<ServiceResponseDto>.CreateSuccess(responseDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(string id)
        {
            var result = await _serviceService.DeleteServiceAsync(id);
            if (!result)
                return NotFound(ApiResponse<ServiceResponseDto>.CreateError(new ErrorResponse
                {
                    Code = ErrorCodes.NotFound,
                    Message = "Service not found"
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