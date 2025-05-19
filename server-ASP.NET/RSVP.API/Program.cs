using Microsoft.EntityFrameworkCore;
using RSVP.API.Middleware;
using RSVP.Infrastructure.Data;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Interfaces.Services;
using RSVP.Infrastructure.Repositories;
using RSVP.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
// = ASP.NET Core 애플리케이션 구성을 위한 빌더 객체 생성, 이 빌더는 서비스 등록, 구성 설정 등을 위해 사용
// = It's for ASP.NET Core application configuration

// Add services to the container.
// Entity Framework core 의 데이터베이스 컨텍스트를 서비스로 등록.  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
// sql server 사용하도록 설정
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

// * MVC pattern 컨트롤러 사용
// * it's parsing the http request data to C# object
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
    options.AddPolicy("ProdCors", policy =>
        policy.WithOrigins("https://myapp.com") // todo: need to change later(for production)
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// api 엔드포인트 정보 생성
builder.Services.AddEndpointsApiExplorer();
// * api 문서 생성 서비스 (아직 사용 안하는중)
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
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
}
else
{
    app.UseCors("ProdCors");
}
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