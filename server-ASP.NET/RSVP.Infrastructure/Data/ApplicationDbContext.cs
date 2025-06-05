using Microsoft.EntityFrameworkCore;
using RSVP.Core.Models;

namespace RSVP.Infrastructure.Data;

// ApplicationDbContext: Entity Framework Core에서 데이터베이스와 상호작용하는 클래스
// ApplicationDbContext: A class in Entity Framework Core that interacts with the database
// - 데이터베이스 연결 관리 (Manages database connections)
// - 엔티티 변경 추적 (Tracks entity changes)
// - 쿼리 실행 및 결과 캐싱 (Executes queries and caches results)
// - 트랜잭션 관리 (Manages transactions)

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreHour> StoreHours { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<StoreService> StoreServices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // todo: need to review with new model type configuration
        base.OnModelCreating(modelBuilder);

        // User 테이블 설정
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FullName).IsRequired();
            entity.Property(e => e.Role).HasDefaultValue("Admin");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Store configuration
        modelBuilder.Entity<Store>(entity =>
        {
            // * HasKey = Primary Key Setup
            // * Property = Column Setup (e.g. Name, Address, Phone, Email)
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StoreId).IsUnique();
            // HasIndex: 데이터베이스 인덱스를 생성하는 메서드. 검색 속도를 향상시키는 데이터 구조를 만듦. 해당 값으로 검색한다는 뜻
            // HasIndex: A method to create a database index. Creates a data structure that improves search speed. it means search by that value
            // IsUnique: 해당 인덱스가 고유해야 함을 지정. 동일한 StoreId 값을 가진 두 개의 Store 레코드가 존재할 수 없음
            // IsUnique: Specifies that the index must be unique. Two Store records with the same StoreId cannot exist
            // 일반적인 설정 순서: 먼저 Property()로 컬럼 특성 설정 후 HasIndex()로 인덱스 설정
            // Normal configuration order: First set column characteristics with Property(), then set index with HasIndex()
            entity.Property(e => e.StoreId).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.StoreId).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);

            // Seed Data
            entity.HasData(
                new Store
                {
                    Id = 1,
                    StoreId = "store-1",
                    Name = "Hair Salon A",
                    Location = "Los Angeles",
                    Email = "hairsalon@example.com"
                },
                new Store
                {
                    Id = 2,
                    StoreId = "store-2",
                    Name = "Hair Salon B",
                    Location = "Texas",
                    Email = "hairsalon@example.com"
                },
                new Store
                {
                    Id = 3,
                    StoreId = "store-3",
                    Name = "Hair Salon C",
                    Location = "New York",
                    Email = "hairsalon@example.com"
                }
            );
        });

        // StoreHour configuration
        modelBuilder.Entity<StoreHour>(entity =>
        {
            // * HasKey = 기본 키 설정 (Primary Key Setup)
            entity.HasKey(e => e.Id);
            // * Property = 컬럼 설정 (Column Setup)
            // StoreId 필드는 필수이며 최대 길이는 100자
            // StoreId field is required and has a maximum length of 100 characters
            entity.Property(e => e.StoreId).IsRequired().HasMaxLength(100);

            // * HasIndex = 데이터베이스 인덱스 생성 (Creates a database index)
            // * IsUnique = 유니크 인덱스 설정 (Sets a unique index)
            // 유니크 인덱스: 동일한 StoreId 값을 가진 두 개의 StoreHour 레코드가 존재할 수 없음
            // Unique index: Two StoreHour records with the same StoreId cannot exist
            // 예: "store1"이라는 StoreId를 가진 StoreHour가 이미 있다면, 다른 StoreHour는 "store1"을 StoreId로 가질 수 없음
            // Example: If a StoreHour with StoreId "store1" already exists, another StoreHour cannot have "store1" as its StoreId
            entity.HasIndex(e => e.StoreId).IsUnique();

            // * HasOne = 일대다 관계 설정 (One-to-Many Relationship). 현재 엔티티가 store를 참조.
            // * HasOne(e => e.Store): StoreHour는 하나의 Store를 가짐 (일 쪽 설정)
            // * WithMany(s => s.StoreHours): Store는 여러 StoreHour를 가짐 (다 쪽 설정)
            // * OnDelete = 삭제 동작 설정, Cascade = (부모 엔티티(Store)가 삭제될 때 자식 엔티티(StoreHour)가 함께 삭제됨)
            // * 관계 방향 판단: 외래 키(StoreId)를 가진 쪽이 자식(StoreHour), 참조되는 쪽이 부모(Store)
            // * ex) HasForeignKey(e=> e.StoreId) 보면 현재 엔티티가 StoreId를 외래 키로 가지고 있으며, 이 키는 Store 엔티티의 기본 storeId를 참조함

            // * HasOne/WithOne = 일대일 관계 설정 (One-to-One Relationship)
            // * HasOne(e => e.Store): StoreHour는 하나의 Store를 참조
            // * WithOne(s => s.StoreHour): Store도 하나의 StoreHour만 참조
            // * HasForeignKey<StoreHour>: 일대일 관계에서는 외래 키를 가진 엔티티 타입을 명시적으로 지정해야 함. 여기서는 해당 엔티티가 store를 참조중이니, StoreHour이 외래 키를 가짐 = 자식
            // * HasOne/WithOne = One-to-One Relationship configuration
            // * HasOne(e => e.Store): StoreHour references one Store
            // * WithOne(s => s.StoreHour): Store also references only one StoreHour
            // * HasForeignKey<StoreHour>: In one-to-one relationships, must explicitly specify which entity owns the foreign key
            entity.HasOne(e => e.Store)
                .WithOne(s => s.StoreHour)
                .HasPrincipalKey<Store>(e => e.StoreId)
                .HasForeignKey<StoreHour>(e => e.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.OwnsMany(e => e.RegularHours, regularHour =>
            {
                regularHour.WithOwner();
                // 종속됨을 의미 + 해당 엔티티의 ID를 가져와 FK 로 설정. 이름은 엔티티 이름 + 기본키 이름이 될 것. ex) StoreHourId
                regularHour.HasKey(r => r.Id);
                regularHour.Property<int>("Id").ValueGeneratedOnAdd();
                // regularHour.HasKey("Id");
                regularHour.HasIndex("StoreHourId", nameof(RegularHour.Day)).IsUnique();
                regularHour.Property(r => r.Day).IsRequired();
                regularHour.Property(r => r.Open).IsRequired();
                regularHour.Property(r => r.Close).IsRequired();
                regularHour.Property(r => r.IsClosed).IsRequired();

                // Seed Data for all stores
                regularHour.HasData(
                    // Store-1 Regular Hours
                    new RegularHour { Id = 1, StoreHourId = 1, Day = DayOfWeek.Monday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 2, StoreHourId = 1, Day = DayOfWeek.Tuesday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 3, StoreHourId = 1, Day = DayOfWeek.Wednesday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 4, StoreHourId = 1, Day = DayOfWeek.Thursday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 5, StoreHourId = 1, Day = DayOfWeek.Friday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 6, StoreHourId = 1, Day = DayOfWeek.Saturday, Open = new TimeSpan(10, 0, 0), Close = new TimeSpan(17, 0, 0), IsClosed = false },
                    new RegularHour { Id = 7, StoreHourId = 1, Day = DayOfWeek.Sunday, Open = new TimeSpan(0, 0, 0), Close = new TimeSpan(0, 0, 0), IsClosed = true },

                    // Store-2 Regular Hours
                    new RegularHour { Id = 8, StoreHourId = 2, Day = DayOfWeek.Monday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 9, StoreHourId = 2, Day = DayOfWeek.Tuesday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 10, StoreHourId = 2, Day = DayOfWeek.Wednesday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 11, StoreHourId = 2, Day = DayOfWeek.Thursday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 12, StoreHourId = 2, Day = DayOfWeek.Friday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 13, StoreHourId = 2, Day = DayOfWeek.Saturday, Open = new TimeSpan(10, 0, 0), Close = new TimeSpan(17, 0, 0), IsClosed = false },
                    new RegularHour { Id = 14, StoreHourId = 2, Day = DayOfWeek.Sunday, Open = new TimeSpan(0, 0, 0), Close = new TimeSpan(0, 0, 0), IsClosed = true },

                    // Store-3 Regular Hours
                    new RegularHour { Id = 15, StoreHourId = 3, Day = DayOfWeek.Monday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 16, StoreHourId = 3, Day = DayOfWeek.Tuesday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 17, StoreHourId = 3, Day = DayOfWeek.Wednesday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 18, StoreHourId = 3, Day = DayOfWeek.Thursday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 19, StoreHourId = 3, Day = DayOfWeek.Friday, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(18, 0, 0), IsClosed = false },
                    new RegularHour { Id = 20, StoreHourId = 3, Day = DayOfWeek.Saturday, Open = new TimeSpan(10, 0, 0), Close = new TimeSpan(17, 0, 0), IsClosed = false },
                    new RegularHour { Id = 21, StoreHourId = 3, Day = DayOfWeek.Sunday, Open = new TimeSpan(0, 0, 0), Close = new TimeSpan(0, 0, 0), IsClosed = true }
                );
            });

            // SpecialDate 설정
            entity.OwnsMany(e => e.SpecialDate, specialDate =>
            {
                specialDate.WithOwner();
                specialDate.HasKey(r => r.Id);
                specialDate.Property<int>("Id").ValueGeneratedOnAdd();
                // specialDate.HasKey("Id");
                specialDate.HasIndex("StoreHourId", nameof(SpecialDate.Date)).IsUnique();
                specialDate.Property(s => s.Date).IsRequired();
                specialDate.Property(s => s.Open).IsRequired();
                specialDate.Property(s => s.Close).IsRequired();
                specialDate.Property(s => s.IsClosed).IsRequired();
            });

            // Seed Data
            entity.HasData(
                new StoreHour { Id = 1, StoreId = "store-1" },
                new StoreHour { Id = 2, StoreId = "store-2" },
                new StoreHour { Id = 3, StoreId = "store-3" }
            );
        });

        // Service configuration
        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ServiceId).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.ServiceId).IsUnique();
            entity.HasAlternateKey(e => e.ServiceId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Duration).IsRequired();
            // HasPrecision(18, 2): decimal 타입의 숫자 형식을 정의. 18은 총 자릿수, 2는 소수점 이하 자릿수를 의미함
            // HasPrecision(18, 2): Defines the numeric format of decimal type. 18 is the total number of digits, 2 is the number of decimal places
            entity.Property(e => e.Price).HasPrecision(18, 2);
            // entity.HasOne(e => e.Store)
            //     .WithMany(s => s.Services)
            //     .HasForeignKey(e => e.StoreId)
            //     .HasPrincipalKey(e => e.StoreId)
            //     // * HasForeignKey = 외래 키 설정 (Foreign Key Setup)
            //     // * Service 엔티티가 Store 엔티티를 참조하기 위한 외래 키로 StoreId 속성을 사용
            //     // * Service entity uses StoreId property as a foreign key to reference the Store entity
            //     .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            entity.HasData(
                new Service
                {
                    Id = 1,
                    ServiceId = "service-1",
                    Name = "Haircut",
                    Duration = 30,
                    Price = 30.00m,

                },
                new Service
                {
                    Id = 2,
                    ServiceId = "service-2",
                    Name = "Hair Coloring",
                    Duration = 120,
                    Price = 80.00m,
                }
            );
        });

        modelBuilder.Entity<StoreService>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ServiceId });

            entity.HasOne(e => e.Store)
                .WithMany(s => s.StoreServices)
                .HasForeignKey(e => e.StoreId)
                .HasPrincipalKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Service)
                .WithMany(s => s.StoreServices)
                .HasForeignKey(e => e.ServiceId)
                .HasPrincipalKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new StoreService { StoreId = "store-1", ServiceId = "service-1" },
                new StoreService { StoreId = "store-1", ServiceId = "service-2" },
                new StoreService { StoreId = "store-2", ServiceId = "service-1" },
                new StoreService { StoreId = "store-2", ServiceId = "service-2" },
                new StoreService { StoreId = "store-3", ServiceId = "service-1" },
                new StoreService { StoreId = "store-3", ServiceId = "service-2" }
            );
        });

        // Reservation configuration
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CustomerPhone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CustomerEmail).HasMaxLength(100);
            entity.Property(e => e.AgreedToTerms).IsRequired();
            entity.HasOne(e => e.Store)
                .WithMany(s => s.Reservations)
                .HasForeignKey(e => e.StoreId)
                .HasPrincipalKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Service)
                .WithMany(s => s.Reservations)
                .HasForeignKey(e => e.ServiceId)
                .HasPrincipalKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });


    }
}