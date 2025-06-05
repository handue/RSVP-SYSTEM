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
        public async Task<ActionResult<ServiceResponseDto>> CreateService([FromBody] CreateServiceDto ServiceDto)
        {
            
            var createdServiceDto = await _serviceService.CreateServiceAsync(ServiceDto);

            return CreatedAtAction(nameof(GetServiceById), new { id = createdServiceDto.ServiceId }, ApiResponse<ServiceResponseDto>.CreateSuccess(createdServiceDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponseDto>> GetServiceById(string id)
        {
            var serviceDto = await _serviceService.GetServiceByIdAsync(id);


            return Ok(ApiResponse<ServiceResponseDto>.CreateSuccess(serviceDto));
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<ServiceResponseDto>>> GetServicesByStoreId(string storeId)
        {
            var serviceDtos = await _serviceService.GetServicesByStoreIdAsync(storeId);

            return Ok(ApiResponse<IEnumerable<ServiceResponseDto>>.CreateSuccess(serviceDtos));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceResponseDto>>> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();

            return Ok(ApiResponse<IEnumerable<ServiceResponseDto>>.CreateSuccess(services));
        }

        // [HttpPut("{id}")]
        // public async Task<ActionResult<ServiceResponseDto>> UpdateService(string id, [FromBody] CreateServiceDto CreateServiceDto)
        // {

        //     CreateServiceDto.ServiceId = id;

        //     var updatedServiceDto = await _serviceService.UpdateServiceAsync(CreateServiceDto);


        //     return Ok(ApiResponse<ServiceResponseDto>.CreateSuccess(updatedServiceDto));
        // }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(string id)
        {
            await _serviceService.DeleteServiceAsync(id);
          
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