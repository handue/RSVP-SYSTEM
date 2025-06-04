using Microsoft.EntityFrameworkCore;
using RSVP.API.Middleware;
using RSVP.Infrastructure.Data;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Interfaces.Services;
using RSVP.Infrastructure.Repositories;
using RSVP.Infrastructure.Services;
using RSVP.Core.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;


Env.Load();

var builder = WebApplication.CreateBuilder(args);
// = ASP.NET Core 애플리케이션 구성을 위한 빌더 객체 생성, 이 빌더는 서비스 등록, 구성 설정 등을 위해 사용
// = It's for ASP.NET Core application configuration

// Add services to the container.
// Entity Framework core 의 데이터베이스 컨텍스트를 서비스로 등록.  

// * 기존 셋업은 이거였으나, 이건 SQL Server LocalDB로 윈도우에서만 지원하는거라 맥 기준으로 현재 바꿔놨음. "Server=(localdb)\\mssqllocaldb;Database=RSVPDb;Trusted_Connection=True;MultipleActiveResultSets=true"

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
// // sql server 사용하도록 설정
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Console.WriteLine("데이터 확인:" + Env.GetString("DB_CONNECTION"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(Env.GetString("DB_CONNECTION"),
        b => b.MigrationsAssembly("RSVP.API")));
// ? MigrationsAssembly 를 사용하는 이유
// ? 현재 우리 프로젝트는 다중 계층 아키텍처로 구성되어 있는데, ApplicationDbContext 는 Infrastucture 계층에 존재. 다만 마이그레이션 명령은 API 프로젝트에서 실행. 그래서 Ef core는 dbcontext가 정의된 프로젝트에 마이그레이션 파일을 생성하려고 시도함. 하지만 앞서 말했듯 스타트업 프로젝트(실행 가능한 프로젝트)는 RSVP.API 이기에 Entity Framework core 도구는 스타트업 프로젝트에서 실행됨. 그러나 DbContext 는 다른 프로젝트에 있어서 마이그레이션 파일이 어디에 생겨야 하는지 혼란이 생김
// ? 그래서 MigrationsAssembly 를 사용하여 마이그레이션 파일이 생성될 위치를 지정해줌. 이 의미는 RSVP.API에 해당 db를 생성한다는 의미

// ? Why we use MigrationAssembly
// ? Our project is structured with a multi-layered architecture, where ApplicationDbContext exists in the Infrastructure layer. However, migration commands are executed from the API project. By default, EF Core attempts to create migration files in the project where the DbContext is defined. But as mentioned, the startup project (executable project) is RSVP.API, so Entity Framework Core tools run from this startup project. Since the DbContext is in a different project, confusion arises about where migration files should be generated.
// ? Therefore, we use MigrationsAssembly to specify where migration files should be created. This means we're instructing EF Core to create the database migrations in RSVP.API.

// * 인터페이스와 그 구현체를 나누는 DIP(의존성 역전 원칙) 패턴의 장점
// * 1. 코드 결합도를 낮출 수 있음 2. 테스트 용이성 증가 3. 유지보수 용이(구현체만 바꾸면 됨)

// Register Repositories
// * AddScoped = one instance per http Request
// * other types = 1. addtransient = every time a new instance is created. 2. AddSingleton = one instance for the lifetime of the application(for all requests). 
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
// * first params = interface, second params = implementation
// * first params means the type when you request the service(normally interface), second params means the implementation of the first params. 
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// Register Services
builder.Services.AddScoped<IStoreService, Store_Service>();
builder.Services.AddScoped<IServiceService, Service_Service>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,  // Whether to validate the token issuer
        ValidateAudience = true,  // Whether to validate the token audience
        ValidateLifetime = true,  // Whether to validate the token expiration time
        ValidateIssuerSigningKey = true,  // Whether to validate the token signing key

        ValidIssuer = Env.GetString("JWT_ISSUER"),  // Valid token issuer
        // Env.GetString = get the value and parse it as string from the environment variable
        // Environment.GetEnvironmentVariable = get the value from the environment variable
        ValidAudience = Env.GetString("JWT_AUDIENCE"),  // Valid token audience

        // Secret key used for token signing
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Env.GetString("JWT_KEY") ?? throw new InvalidOperationException("JWT Key not found")))
    };
});

// * MVC pattern 컨트롤러 사용
// * it's parsing the http request data to C# object
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
    );
    options.AddPolicy("ProdCors", policy =>
        policy.WithOrigins("https://myapp.com") // todo: need to change later(for production)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
    );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// api 엔드포인트 정보 생성
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 등록된 서비스와 설정으로 웹 애플리케이션 빌드
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

// Add ReDoc for better readability
app.UseReDoc(c =>
{
    c.RoutePrefix = "redoc";
    c.SpecUrl("/swagger/v1/swagger.json");
    c.DocumentTitle = "RSVP API Documentation";
    c.HideHostname();
    // c.HideDownloadButton(); 
    c.RequiredPropsFirst();
    c.SortPropsAlphabetically();
});

// redirect http to https


if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
}
else
{
    app.UseHttpsRedirection();
    app.UseCors("ProdCors");
}

app.UseRouting();

// 인증관리, 미들웨어나 라우터에 [] 로 넣어주면 되는데 지금은 구현 안됐음
app.UseAuthorization();

// Add global exception handling middleware
app.UseMiddleware<GlobalExceptionMiddleware>();
// * Addcontrollers 는 서비스 등록 단계에서 호출하여, DI 컨테이너에 컨트롤러 관련 서비스를 등록
// * MapControllers 는 애플리케이션 구성 단계에서 호출, 컨트롤러에 대한 라우팅 규칙을 설정.
app.MapControllers();

app.Run();

// class vas record
// ? class = value type, record = reference type
// ex) var person 1 = new PersonRecord("John", 30);
// ex) var person 2 = new PersonRecord("John", 30);
// ex) Console.WriteLine(person1 == person2); // true (value type is equal)
// ex) but if it's just class, it's false because the object is different. 

// * 즉 class = 같은 객체인지, record = 속성의 값이 같은지