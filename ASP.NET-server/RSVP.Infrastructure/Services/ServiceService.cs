using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

namespace RSVP.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IReservationRepository _reservationRepository;

        public ServiceService(
            IServiceRepository serviceRepository,
            IStoreRepository storeRepository,
            IReservationRepository reservationRepository)
        {
            _serviceRepository = serviceRepository;
            _storeRepository = storeRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<Service> CreateServiceAsync(Service service)
        {
            // 1. 매장이 존재하는지 확인
            var store = await _storeRepository.GetByStoreIdAsync(service.StoreId);
            if (store == null)
                throw new ArgumentException($"Store with ID {service.StoreId} not found.");

            // 2. 서비스 ID 중복 확인
            if (await _serviceRepository.ExistsByServiceIdAsync(service.ServiceId))
                throw new ArgumentException($"Service with ID {service.ServiceId} already exists.");

            // 3. 서비스 생성
            return await _serviceRepository.AddAsync(service);
        }

        public async Task<Service> GetServiceByIdAsync(string id)
        {
            return await _serviceRepository.GetByServiceIdAsync(id);
        }

        public async Task<IEnumerable<Service>> GetServicesByStoreIdAsync(string storeId)
        {
            return await _serviceRepository.GetServicesByStoreIdAsync(storeId);
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _serviceRepository.GetAllAsync();
        }

        public async Task<Service> UpdateServiceAsync(Service service)
        {
            // 1. 서비스가 존재하는지 확인
            var existingService = await _serviceRepository.GetByServiceIdAsync(service.ServiceId);
            if (existingService == null)
                throw new ArgumentException($"Service with ID {service.ServiceId} not found.");

            // 2. 매장이 존재하는지 확인
            var store = await _storeRepository.GetByStoreIdAsync(service.StoreId);
            if (store == null)
                throw new ArgumentException($"Store with ID {service.StoreId} not found.");

            // 3. 서비스 업데이트
            return _serviceRepository.Update(service);
        }

        public async Task<bool> DeleteServiceAsync(string id)
        {
            var service = await _serviceRepository.GetByServiceIdAsync(id);
            if (service == null)
                return false;

            _serviceRepository.Remove(service);
            return true;
        }

        public async Task<bool> IsServiceAvailableAsync(string serviceId, DateTime date, TimeSpan time)
        {
            // 1. 서비스 확인
            var service = await _serviceRepository.GetByServiceIdAsync(serviceId);
            if (service == null)
                return false;

            // 2. 매장 영업시간 확인
            var store = await _storeRepository.GetByStoreIdAsync(service.StoreId);
            if (store == null)
                return false;

            // 3. 예약 가능 시간인지 확인
            var reservations = await _reservationRepository.GetReservationsByDateAsync(date);
            var conflictingReservations = reservations
                .Where(r => r.ServiceId == serviceId && 
                           r.Time <= time && 
                           r.Time.Add(r.Duration) > time)
                .ToList();

            return !conflictingReservations.Any();
        }
    }
} 