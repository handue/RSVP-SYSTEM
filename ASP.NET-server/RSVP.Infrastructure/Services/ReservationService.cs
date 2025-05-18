using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;

namespace RSVP.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceRepository _serviceRepository;

        public ReservationService(
            IReservationRepository reservationRepository,
            IStoreRepository storeRepository,
            IServiceRepository serviceRepository)
        {
            _reservationRepository = reservationRepository;
            _storeRepository = storeRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            // 1. 매장과 서비스가 존재하는지 확인
            var store = await _storeRepository.GetByStoreIdAsync(reservation.StoreId);
            if (store == null)
                throw new ArgumentException($"Store with ID {reservation.StoreId} not found.");

            var service = await _serviceRepository.GetByServiceIdAsync(reservation.ServiceId);
            if (service == null)
                throw new ArgumentException($"Service with ID {reservation.ServiceId} not found.");

            // 2. 예약 가능 시간인지 확인
            // if (!await IsTimeSlotAvailableAsync(reservation.StoreId, reservation.ServiceId, 
            //     reservation.Date, reservation.Time))
            //     throw new InvalidOperationException("The selected time slot is not available.");

            // 3. 예약 생성
            return await _reservationRepository.AddAsync(reservation);
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByStoreIdAsync(string storeId)
        {
            return await _reservationRepository.GetReservationsByStoreIdAsync(storeId);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByServiceIdAsync(string serviceId)
        {
            return await _reservationRepository.GetReservationsByServiceIdAsync(serviceId);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _reservationRepository.GetReservationsByDateAsync(date);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerEmailAsync(string email)
        {
            return await _reservationRepository.GetReservationsByCustomerEmailAsync(email);
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            // 1. 기존 예약이 존재하는지 확인
            var existingReservation = await _reservationRepository.GetByIdAsync(reservation.Id);
            if (existingReservation == null)
                throw new ArgumentException($"Reservation with ID {reservation.Id} not found.");

            // // 2. 시간 변경이 있는 경우, 새로운 시간이 가능한지 확인
            // if (existingReservation.Date != reservation.Date || 
            //     existingReservation.Time != reservation.Time)
            // {
            //     if (!await IsTimeSlotAvailableAsync(reservation.StoreId, reservation.ServiceId, 
            //         reservation.Date, reservation.Time))
            //         throw new InvalidOperationException("The selected time slot is not available.");
            // }

            // 3. 예약 업데이트
            return await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                return false;

            await _reservationRepository.RemoveAsync(reservation);
            return true;
        }

        // public async Task<bool> IsTimeSlotAvailableAsync(string storeId, string serviceId, 
        //     DateTime date, TimeSpan time)
        // {
        //     // 1. 매장의 영업 시간 확인
        //     var store = await _storeRepository.GetByStoreIdAsync(storeId);
        //     if (store == null)
        //         return false;

        //     // 2. 서비스의 소요 시간 확인
        //     var service = await _serviceRepository.GetByServiceIdAsync(serviceId);
        //     if (service == null)
        //         return false;

        //     // 3. 해당 시간대의 예약 확인
        //     var reservations = await _reservationRepository.GetReservationsByDateAsync(date);
        //     var conflictingReservations = reservations
        //         .Where(r => r.StoreId == storeId && 
        //                    r.Time <= time && 
        //                    r.Time.Add(r.Duration) > time)
        //         .ToList();

        //     return !conflictingReservations.Any();
        // }
    }
} 