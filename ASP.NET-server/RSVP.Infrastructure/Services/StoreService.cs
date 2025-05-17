using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

// TODO : 수정 꼭 필요. 아직 터칭 안했음
namespace RSVP.Infrastructure.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IReservationRepository _reservationRepository;

        public StoreService(
            IStoreRepository storeRepository,
            IServiceRepository serviceRepository,
            IReservationRepository reservationRepository)
        {
            _storeRepository = storeRepository;
            _serviceRepository = serviceRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<Store> CreateStoreAsync(Store store)
        {
            // 1. 매장 ID 중복 확인
            if (await _storeRepository.ExistsByStoreIdAsync(store.StoreId))
                throw new ArgumentException($"Store with ID {store.StoreId} already exists.");

            // 2. 매장 생성
            return await _storeRepository.AddAsync(store);
        }

        public async Task<Store> GetStoreByIdAsync(string id)
        {
            return await _storeRepository.GetByStoreIdAsync(id);
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Store>> GetStoresByLocationAsync(string location)
        {
            return await _storeRepository.GetStoresByLocationAsync(location);
        }

        public async Task<Store> UpdateStoreAsync(Store store)
        {
            // 1. 매장이 존재하는지 확인
            var existingStore = await _storeRepository.GetByStoreIdAsync(store.StoreId);
            if (existingStore == null)
                throw new ArgumentException($"Store with ID {store.StoreId} not found.");

            // 2. 매장 업데이트
            return _storeRepository.Update(store);
        }

        public async Task<bool> DeleteStoreAsync(string id)
        {
            var store = await _storeRepository.GetByStoreIdAsync(id);
            if (store == null)
                return false;

            _storeRepository.Remove(store);
            return true;
        }

        public async Task<bool> IsStoreOpenAsync(string storeId, DateTime date, TimeSpan time)
        {
            var store = await _storeRepository.GetByStoreIdAsync(storeId);
            if (store == null)
                return false;

            // 1. 특별 영업일 확인
            var specialDate = store.SpecialDates
                .FirstOrDefault(sd => sd.Date.Date == date.Date);
            
            if (specialDate != null)
            {
                return time >= specialDate.OpenTime && time <= specialDate.CloseTime;
            }

            // 2. 일반 영업시간 확인
            var dayOfWeek = date.DayOfWeek;
            var regularHours = store.RegularHours
                .FirstOrDefault(rh => rh.DayOfWeek == dayOfWeek);

            if (regularHours == null)
                return false;

            return time >= regularHours.OpenTime && time <= regularHours.CloseTime;
        }

        public async Task<IEnumerable<TimeSpan>> GetAvailableTimeSlotsAsync(
            string storeId, string serviceId, DateTime date)
        {
            // 1. 매장과 서비스 확인
            var store = await _storeRepository.GetByStoreIdAsync(storeId);
            if (store == null)
                throw new ArgumentException($"Store with ID {storeId} not found.");

            var service = await _serviceRepository.GetByServiceIdAsync(serviceId);
            if (service == null)
                throw new ArgumentException($"Service with ID {serviceId} not found.");

            // 2. 영업 시간 확인
            if (!await IsStoreOpenAsync(storeId, date, TimeSpan.Zero))
                return Enumerable.Empty<TimeSpan>();

            // 3. 예약 가능 시간대 계산
            var availableSlots = new List<TimeSpan>();
            var currentTime = store.RegularHours
                .FirstOrDefault(rh => rh.DayOfWeek == date.DayOfWeek)?.OpenTime 
                ?? TimeSpan.Zero;
            
            var closeTime = store.RegularHours
                .FirstOrDefault(rh => rh.DayOfWeek == date.DayOfWeek)?.CloseTime 
                ?? TimeSpan.Zero;

            // 4. 기존 예약 확인
            var existingReservations = await _reservationRepository.GetReservationsByDateAsync(date);
            var storeReservations = existingReservations
                .Where(r => r.StoreId == storeId)
                .ToList();

            // 5. 가능한 시간대 계산
            while (currentTime.Add(service.Duration) <= closeTime)
            {
                var isAvailable = true;
                foreach (var reservation in storeReservations)
                {
                    if (currentTime < reservation.Time.Add(reservation.Duration) &&
                        currentTime.Add(service.Duration) > reservation.Time)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                    availableSlots.Add(currentTime);

                currentTime = currentTime.Add(TimeSpan.FromMinutes(30)); // 30분 간격으로 체크
            }

            return availableSlots;
        }
    }
} 