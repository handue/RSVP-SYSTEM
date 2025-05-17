# RSVP System

A reservation management system with React/TypeScript frontend and ASP.NET Core backend.

## Project Structure

This project is organized as a monorepo with the following structure:

- `client/`: React/TypeScript frontend application
- `ASP.NET-server/`: ASP.NET Core backend server (current implementation)
- `spring-server/`: Spring Boot backend server (planned for future development)

## Current Development Focus

The project is being developed in phases:

- **Phase 1**: Frontend implementation with React/TypeScript
- **Phase 2 (Current)**: Backend implementation with ASP.NET Core
- **Phase 3 (Planned)**: Alternative backend implementation with Spring Boot

## Features

### Current Features (Frontend)

- User-friendly reservation interface
  - 3-step reservation process (Store → Service → Details)
  - Progress tracking with Steps component
  - Store and service selection interface
- Date and time slot selection
- Store hours management
- Admin dashboard for reservation management
- Mock API integration for development

### Current Features (Backend - ASP.NET Core)

- RESTful API endpoints
- Entity Framework Core for data access
- Repository pattern implementation
- Service layer with business logic
- Domain models with validation
- Clean architecture principles

### Planned Features (Backend - Spring Boot)

- Alternative RESTful API implementation
- Spring Data JPA for data access
- Spring Security for authentication
- Email notification system
- Integration with Google APIs

## Technology Stack

### Frontend

- React 18
- TypeScript
- Redux Toolkit for state management
- React Router for navigation
- Tailwind CSS for styling
- Vite as build tool

### Backend (Current - ASP.NET Core)

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Repository Pattern
- Clean Architecture
- JWT Authentication

### Backend (Planned - Spring Boot)

- Spring Boot
- PostgreSQL or MySQL
- Spring Security (JWT)
- Lombok
- Maven
- Google API Client (Gmail, Calendar, Sheets)

## Getting Started

### Running the Frontend

```bash
# Navigate to client directory
cd client

# Install dependencies
npm install

# Start development server
npm run dev
```

The frontend will be available at `http://localhost:5173`.

### Running the Backend (ASP.NET Core)

```bash
# Navigate to ASP.NET-server directory
cd ASP.NET-server

# Run the application
dotnet run --project RSVP.API
```

The backend API will be available at `http://localhost:5000`.

## Frontend Architecture

The client application follows a feature-based architecture:

- **Components**: 
  - `/components/ui/common`: Common UI components (Steps, Button, etc.)
  - `/components/ui/reservation`: Reservation-related components
  - `/components/ui/admin`: Admin dashboard components
- **Pages**: 
  - `/pages/reservation`: Reservation-related pages
  - `/pages/admin`: Admin dashboard pages
- **Services**: API service functions for data fetching
- **Hooks**: Custom React hooks for shared logic
- **Types**: TypeScript type definitions
  - Reservation types
  - Store types
  - Service types
- **Utils**: Utility functions for common operations

## Backend Architecture (ASP.NET Core)

The backend follows clean architecture principles:

- **API Layer**: Controllers and API endpoints
- **Core Layer**: Domain models and interfaces
- **Infrastructure Layer**: Repository implementations and data access
- **Service Layer**: Business logic implementation

## Development Workflow

This project follows a simplified Git workflow:

- `main` branch contains the stable version
- Feature branches are used for new features and bug fixes
- Semantic commit messages are used for better readability

## Steps

- Complete the ASP.NET Core backend implementation
- Integrate frontend with ASP.NET Core backend
- Implement authentication and authorization
- Deploy the application
- Develop alternative Spring Boot backend (future)
