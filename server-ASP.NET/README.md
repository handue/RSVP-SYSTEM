# RSVP System

A reservation management system with React/TypeScript frontend and ASP.NET Core backend.

## Project Structure

This project is organized as a monorepo with the following structure:

- `client/`: React/TypeScript frontend application
- `server-ASP.NET/`: ASP.NET Core backend server
- `Images/`: Project documentation images

## Features

### Current Features (Frontend)
- User-friendly reservation interface
  - 3-step reservation process (Store → Service → Details)
  - Progress tracking with Steps component
  - Store and service selection interface
- Date and time slot selection
- Store hours management
- Admin dashboard for reservation management
- Google Calendar integration
- Email notification system

### Current Features (Backend - ASP.NET Core)
- RESTful API endpoints
- Entity Framework Core for data access
- Repository pattern implementation
- Service layer with business logic
- Domain models with validation
- Clean architecture principles
- Google Calendar API integration
- Gmail API integration
- Swagger/OpenAPI documentation
- JWT Authentication

## Technology Stack

### Frontend
- React 18
- TypeScript
- Redux Toolkit for state management
- React Router for navigation
- Tailwind CSS for styling
- Vite as build tool
- React Datepicker
- Lucide React (icons)

### Backend
- ASP.NET Core 8
- Entity Framework Core
- SQLite
- Repository Pattern
- Clean Architecture
- JWT Authentication
- Google API Client
- Swagger/OpenAPI

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQLite
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository and navigate to the server-ASP.NET directory
```bash
cd server-ASP.NET
```

2. Restore dependencies
```bash
dotnet restore
```

3. Update connection string in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=RSVP.db"
  }
}
```

4. Run database migrations
```bash
dotnet ef database update
```

5. Start the development server
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

### User Reservation Flow
- Store selection
- Service type selection
- Date and time slot selection
- Form validation
- Confirmation system
- Google Calendar integration
- Email notifications

### Admin Management
- Store hours configuration
- Special dates management
- Regular hours scheduling
- Reservation management
- User management

## API Integration

The frontend communicates with the backend through the following endpoints:

- Store Hours:
  - `GET /api/store-hours`
  - `PUT /api/store-hours/regular-hours/:storeId`
  - `PUT /api/store-hours/special-date/:storeId`
  - `DELETE /api/store-hours/special-date/:storeId/:date`

- Reservations:
  - `POST /api/reservation/calendar`
  - `POST /api/reservation/send`
  - `GET /api/reservation/:id`
  - `PUT /api/reservation/:id`
  - `DELETE /api/reservation/:id`

- Authentication:
  - `POST /api/auth/login`
  - `POST /api/auth/refresh`

### Authentication

- JWT 기반 인증 (JWT-based authentication)
- 역할 기반 인가 (Role-based authorization)
- 토큰 갱신 메커니즘 (Token refresh mechanism)

### Database

- Entity Framework Core Code First (Similar to JPA/Hibernate in Spring Boot or Mongoose in Node.js)
- SQLite 데이터베이스 (Similar to MySQL/PostgreSQL in Spring Boot or MongoDB in Node.js)
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
