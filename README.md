# RSVP System

A reservation management system with React/TypeScript frontend and planned Spring Boot backend.

## Project Structure

This project is organized as a monorepo with the following structure:

- `client/`: React/TypeScript frontend application
- `spring-server/`: Spring Boot backend server (planned for future development)

## Current Development Focus

The project is being developed in phases:

- **Phase 1 (Current)**: Frontend implementation with React/TypeScript
- **Phase 2 (Planned)**: Backend implementation with Spring Boot

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

### Planned Features (Backend)

- RESTful API endpoints
- Database persistence
- Email notification system
- Authentication and authorization

## Technology Stack

### Frontend

- React 18
- TypeScript
- Redux Toolkit for state management
- React Router for navigation
- Tailwind CSS for styling
- Vite as build tool

### Backend (Planned)

- Spring Boot
- PostgreSQL or MySql
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

## Development Workflow

This project follows a simplified Git workflow:

- `main` branch contains the stable version
- Feature branches are used for new features and bug fixes
- Semantic commit messages are used for better readability

## Next Steps

- Complete the frontend implementation
- Develop the Spring Boot backend
- Integrate frontend with actual backend API
- Implement authentication and authorization
- Deploy the application
