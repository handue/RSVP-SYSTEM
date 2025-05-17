using Microsoft.EntityFrameworkCore;
using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Models;
using RSVP.Infrastructure.Data;

namespace RSVP.Infrastructure.Repositories;

// ReservationRepository: Reservation 객체를 위한 저장소 클래스
// ReservationRepository: Repository class for Reservation objects
// Repository<Reservation>: 기본 CRUD 기능을 상속받음
// Repository<Reservation>: Inherits basic CRUD functionality
// IReservationRepository: 예약에 특화된 추가 메서드를 구현함
// IReservationRepository: Implements additional reservation-specific methods
public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    // base(context): 부모 클래스인 Repository<Reservation>의 생성자를 호출하여 데이터베이스 컨텍스트를 전달함
    // base(context): Calls the constructor of the parent class Repository<Reservation> to pass the database context
    // base : 부모 클래스의 생성자를 호출하는 키워드
    // base : The keyword for calling the constructor of the parent class
    // 사용 예시: 
    // var dbContext = new ApplicationDbContext(options); // DB 컨텍스트 생성
    // var reservationRepo = new ReservationRepository(dbContext); // 레포지토리 초기화
    // 
    // 의존성 주입 사용 시:
    // services.AddScoped<IReservationRepository, ReservationRepository>(); // Startup.cs에 등록
    // 그 후 생성자에서 IReservationRepository를 매개변수로 받아 사용
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByStoreIdAsync(string storeId)
    {
        return await _dbSet
            .Where(r => r.StoreId == storeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByServiceIdAsync(string serviceId)
    {
        return await _dbSet
            .Where(r => r.ServiceId == serviceId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date)
    {
        return await _dbSet
            .Where(r => r.ReservationDate.Date == date.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByCustomerEmailAsync(string email)
    {
        return await _dbSet
            .Where(r => r.CustomerEmail == email)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsWithDetailsAsync()
    {
        return await _dbSet
            .Include(r => r.Store)
            .Include(r => r.Service)
            .ToListAsync();
    }}

// * Not implemented yet, utilize it needed.
//     public async Task<bool> IsTimeSlotAvailableAsync(string storeId, string serviceId, DateTime date, TimeSpan time)
//     {
//         // Services는 ApplicationDbContext의 DbSet<Service> 속성으로, 데이터베이스의 Service 엔티티 컬렉션을 나타냄
//         // Services is a DbSet<Service> property of ApplicationDbContext, representing the collection of Service entities in the database
//         // FirstOrDefaultAsync는 Entity Framework Core에서 제공하는 확장 메서드로, 조건에 맞는 첫 번째 항목을 비동기적으로 가져오거나 없으면 null 반환
//         // FirstOrDefaultAsync is an extension method provided by Entity Framework Core that asynchronously retrieves the first matching item or returns null if none exists
//         var service = await _context.Services
//             .FirstOrDefaultAsync(s => s.ServiceId == serviceId);

//         if (service == null) return false;

//         var endTime = time.Add(TimeSpan.FromMinutes(service.Duration));

//         var conflictingReservations = await _dbSet
//             .Where(r => r.StoreId == storeId &&
//                        r.ServiceId == serviceId &&
//                        r.ReservationDate.Date == date.Date &&
//                        r.Status != ReservationStatus.Cancelled &&
//                        ((r.ReservationTime <= time && r.ReservationTime.Add(TimeSpan.FromMinutes(service.Duration)) > time) ||
//                         (r.ReservationTime < endTime && r.ReservationTime.Add(TimeSpan.FromMinutes(service.Duration)) >= endTime)))
//             .ToListAsync();

//         return !conflictingReservations.Any();
//     }
// } 