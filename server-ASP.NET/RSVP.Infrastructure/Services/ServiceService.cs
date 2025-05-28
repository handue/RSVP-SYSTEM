using AutoMapper;
using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

namespace RSVP.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ServiceService(
            IServiceRepository serviceRepository,
            IStoreRepository storeRepository,
            IReservationRepository reservationRepository,
            IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _storeRepository = storeRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponseDto> CreateServiceAsync(CreateServiceDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);
            // 1. 매장이 존재하는지 확인
            var store = await _storeRepository.GetByStoreIdAsync(service.Store.StoreId);

            if (store == null)
                throw new KeyNotFoundException($"Store with ID {service.Store.StoreId} not found.");

            var isExists = await _serviceRepository.ExistsByServiceIdAsync(service.ServiceId);

            // 2. 서비스 ID 중복 확인
            if (isExists)
                throw new ArgumentException($"Service with ID {service.ServiceId} already exists.");

            // 3. 서비스 생성
            var createdServiceEntity = await _serviceRepository.AddAsync(service);

            var createdServiceDto = _mapper.Map<ServiceResponseDto>(createdServiceEntity);

            return createdServiceDto;
        }

        public async Task<ServiceResponseDto> GetServiceByIdAsync(string id)
        {
            var service = await _serviceRepository.GetByServiceIdAsync(id);

            if (service == null)
                throw new ArgumentException($"Service with ID {id} not found.");

            var serviceDto = _mapper.Map<ServiceResponseDto>(service);

            return serviceDto;
        }

        public async Task<IEnumerable<ServiceResponseDto>> GetServicesByStoreIdAsync(string storeId)
        {
            var services = await _serviceRepository.GetServicesByStoreIdAsync(storeId);

            var serviceDtos = _mapper.Map<IEnumerable<ServiceResponseDto>>(services);

            return serviceDtos;
        }

        public async Task<IEnumerable<ServiceResponseDto>> GetAllServicesAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            var serviceDtos = _mapper.Map<IEnumerable<ServiceResponseDto>>(services);

            return serviceDtos;
        }

        public async Task<ServiceResponseDto> UpdateServiceAsync(CreateServiceDto serviceDto)
        {

            var service = _mapper.Map<Service>(serviceDto);

            // 1. 서비스가 존재하는지 확인
            var existingService = await _serviceRepository.GetByServiceIdAsync(service.ServiceId);
            if (existingService == null)
                throw new ArgumentException($"Service with ID {serviceDto.ServiceId} not found.");

            // 2. 매장이 존재하는지 확인
            var store = await _storeRepository.GetByStoreIdAsync(service.StoreId);
            if (store == null)
                throw new ArgumentException($"Store with ID {serviceDto.StoreId} not found.");

            // 3. 서비스 업데이트
            var updatedServiceEntity = await _serviceRepository.UpdateAsync(service);


            var updatedServiceDto = _mapper.Map<ServiceResponseDto>(updatedServiceEntity);

            return updatedServiceDto;
        }

        public async Task<bool> DeleteServiceAsync(string id)
        {
            var service = await _serviceRepository.GetByServiceIdAsync(id);
            if (service == null)
                throw new KeyNotFoundException($"Service with ID {id} not found.");

            await _serviceRepository.RemoveAsync(service);
            return true;
        }

        // public async Task<bool> IsServiceAvailableAsync(string serviceId, DateTime date, TimeSpan time)
        // {
        //     // 1. 서비스 확인
        //     var service = await _serviceRepository.GetByServiceIdAsync(serviceId);
        //     if (service == null)
        //         return false;

        //     // 2. 매장 영업시간 확인
        //     var store = await _storeRepository.GetByStoreIdAsync(service.StoreId);
        //     if (store == null)
        //         return false;

        //     // 3. 예약 가능 시간인지 확인
        //     var reservations = await _reservationRepository.GetReservationsByDateAsync(date);
        //     var conflictingReservations = reservations
        //         .Where(r => r.ServiceId == serviceId &&
        //                    r.Time <= time &&
        //                    r.Time.Add(r.Duration) > time)
        //         .ToList();

        //     return !conflictingReservations.Any();
        // }
    }
}