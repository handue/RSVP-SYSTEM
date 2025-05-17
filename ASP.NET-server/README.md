# RSVP System - Backend

The backend application for the RSVP System, built with ASP.NET Core 8.0, Entity Framework Core, and SQL Server.

## Technology Stack

- **ASP.NET Core 8.0**: 웹 애플리케이션과 API를 개발하기 위한 크로스 플랫폼 프레임워크입니다. (Cross-platform framework for building web applications and APIs)
- **Entity Framework Core 8.0**: .NET용 ORM(객체 관계형 매핑) 도구로, 데이터베이스와 코드 간의 매핑을 쉽게 해줍니다. (ORM tool for .NET that simplifies database-to-code mapping)
- **SQL Server**: Microsoft의 관계형 데이터베이스 관리 시스템(RDBMS)입니다. (Microsoft's relational database management system)
- **JWT Authentication**: JSON Web Token을 사용한 인증 시스템으로, 사용자 인증 및 권한 부여에 사용됩니다. (Authentication system using JSON Web Tokens)
- **AutoMapper**: 객체 간 매핑을 자동화하는 라이브러리로, DTO와 도메인 모델 간 변환을 쉽게 해줍니다. (Library that automates object-to-object mapping)
- **FluentValidation**: 입력 데이터 유효성 검사를 위한 라이브러리입니다. (Library for input data validation)
- **Swagger/OpenAPI**: API 문서화 도구로, API 엔드포인트를 자동으로 문서화하고 테스트할 수 있게 해줍니다. (API documentation tool)

## Project Structure
```
RSVP.sln
├── RSVP.API/              # Web API 프로젝트
│   ├── Controllers/       # API 엔드포인트 컨트롤러
│   ├── Middleware/        # 커스텀 미들웨어 구현
│   └── Program.cs         # 애플리케이션 설정 및 구성

├── RSVP.Core/             # 핵심 비즈니스 로직 및 도메인 계층
│   ├── Models/            # 도메인 모델 클래스
│   ├── Interfaces/        # 인터페이스 정의
│   └── DTOs/              # 데이터 전송 객체

├── RSVP.Infrastructure/   # 데이터 액세스 및 외부 서비스 계층
│   ├── Data/              # DbContext 및 데이터베이스 마이그레이션
│   ├── Repositories/      # 리포지토리 패턴 구현체
│   └── Services/          # 외부 서비스 통합 구현

└── RSVP.Tests/            # 테스트 프로젝트(Not Used)
    ├── API/               # API 계층 테스트
    ├── Core/              # 도메인 계층 테스트
    └── Infrastructure/    # 인프라 계층 테스트

## Getting Started

### Prerequisites

- .NET 8.0 SDK (Similar to Node.js runtime or Java JDK)
- SQL Server (Similar to MySQL/PostgreSQL in Spring Boot or MongoDB in Node.js)
- Visual Studio 2022 or VS Code (Similar to IntelliJ IDEA for Spring Boot or VS Code for Node.js)

### Installation

1. Clone the repository and navigate to the ASP.NET-server directory
```bash
cd ASP.NET-server
```

2. Restore dependencies (Similar to `npm install` in Node.js or Maven/Gradle dependencies in Spring Boot)
```bash
dotnet restore
```

3. Update connection string in `appsettings.json` (Similar to `application.properties` in Spring Boot or `.env` in Node.js)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RSVP;Trusted_Connection=True;"
  }
}
```

4. Run database migrations (Similar to Flyway/Liquibase in Spring Boot or Mongoose migrations in Node.js)
```bash
dotnet ef database update
```

5. Start the development server (Similar to `npm start` in Node.js or `./mvnw spring-boot:run` in Spring Boot)
```bash
dotnet run --project RSVP.API
```

The API will be available at `https://localhost:7001` and Swagger UI at `https://localhost:7001/swagger`.

## Available Scripts

- `dotnet build` - 프로젝트 빌드 (Similar to `npm run build` or `mvn clean install`)
- `dotnet test` - 테스트 실행 (Similar to `npm test` or `mvn test`)
- `dotnet run --project RSVP.API` - API 서버 실행 (Similar to `npm start` or `mvn spring-boot:run`)
- `dotnet ef migrations add [name]` - 새 마이그레이션 생성 (Similar to `npx prisma migrate` or `mvn flyway:migrate`)
- `dotnet ef database update` - 데이터베이스 업데이트 (Similar to `npx prisma db push` or `mvn flyway:repair`)

## Features

### API Endpoints

- Store Hours:
  - `GET /api/store-hours` - 영업시간 조회 (Get store hours)
  - `PUT /api/store-hours/regular-hours/{storeId}` - 정기 영업시간 수정 (Update regular store hours)
  - `PUT /api/store-hours/special-date/{storeId}` - 특별 영업시간 수정 (Update special store hours)
  - `DELETE /api/store-hours/special-date/{storeId}/{date}` - 특별 영업시간 삭제 (Delete special store hours)

- Reservations:
  - `POST /api/reservation/calendar` - 예약 가능 시간 조회 (Get available reservation times)
  - `POST /api/reservation/send` - 예약 생성 (Create reservation)
  - `GET /api/reservation/{id}` - 예약 상세 조회 (Get reservation details)
  - `PUT /api/reservation/{id}` - 예약 수정 (Update reservation)
  - `DELETE /api/reservation/{id}` - 예약 취소 (Cancel reservation)

### Authentication

- JWT 기반 인증 (JWT-based authentication)
- 역할 기반 인가 (Role-based authorization)
- 토큰 갱신 메커니즘 (Token refresh mechanism)

### Database

- Entity Framework Core Code First (Similar to JPA/Hibernate in Spring Boot or Mongoose in Node.js)
- SQL Server 데이터베이스 (Similar to MySQL/PostgreSQL in Spring Boot or MongoDB in Node.js)
- 마이그레이션 기반 스키마 관리 (Migration-based schema management)

## Contributing

1. 모든 변경사항은 main 브랜치에 직접 커밋 (All changes are committed directly to main branch)
2. 의미 있는 커밋 메시지 사용 (Use meaningful commit messages)
3. 테스트 코드 작성 필수 (Test code is required)

## Commit Message Format

```
<type>(<scope>): <subject>

### Types:
- feat: 새로운 기능 (New feature)
- fix: 버그 수정 (Bug fix)
- docs: 문서 변경 (Documentation changes)
- style: 코드 스타일 변경 (Code style changes)
- refactor: 코드 리팩토링 (Code refactoring)
- test: 테스트 추가/수정 (Test additions/modifications)
- chore: 빌드 프로세스 변경 (Build process changes)
```

## Building for Production

```bash
dotnet publish -c Release
```

빌드 결과물은 `bin/Release/net8.0/publish` 디렉토리에 생성됩니다. (Build artifacts are generated in the `bin/Release/net8.0/publish` directory)
