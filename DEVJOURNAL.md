# Development Journal

This file tracks development decisions and progress for the RSVP System project.

## 2025-05-11(PDT): Project Setup

**Decisions made:**

- Set up monorepo structure with client and server directories
- Chose React with TypeScript for frontend
- Planning Spring Boot for backend (future development)
- Decided to use Redux Toolkit for state management

**GitHub workflow:**

- Using `main` branch as primary branch
- Commit message format: `type(scope): subject`
- Creating GitHub issues for tracking features

**Next steps:**

- Implement basic type definitions
- Set up Redux store structure

## 2025-05-11: Store Configuration and Custom Hooks Implementation

**Implemented:**

- Created TypeScript interfaces for store and reservation
- Set up API configuration and services
- Implemented Redux Toolkit store and slices
- Added custom hooks for store access
- Added proper error handling and loading states

**Technical Decisions:**

- Used Redux Toolkit for state management
- Implemented proper TypeScript types
- Created custom hooks for better code organization
- Added proper error handling

**Next Steps:**

- Implement error handling middleware
- Add proper error handling in components
- Create form validation

## 2025-05-12: UI Components and Reservation System Implementation

**Implemented:**

- Installed and configured shadcn/ui components
- Created reservation form component with validation
- Implemented reservation list view with filtering
- Added reservation detail view
- Set up notification system

**Technical Decisions:**

- Used shadcn/ui for consistent and accessible UI components
- Implemented form validation using HTML5 validation
- Created reusable components for reservation management
- Added proper error handling and loading states
- Used English for all UI text and component names

**Next Steps:**

- Implement admin dashboard
- Add date and time validation
- Implement store hours integration
- Add email notification system

## 2025-05-13: Reservation System Structure Implementation

**Implemented:**

- Created Steps component for reservation progress tracking
- Implemented StoreSelection and ServiceSelection components
- Set up reservation pages structure (ReservationPage, ReservationListPage, ReservationDetailPage)
- Added service type definitions
- Updated existing reservation components (ReservationForm, ReservationList, ReservationDetail)

**Technical Decisions:**

- Used shadcn/ui components for consistent UI
- Implemented 3-step reservation process (Store → Service → Details)
- Separated page components from UI components
- Used TypeScript for type safety
- Maintained English for all component names and UI text

**Next Steps:**

- Implement API integration for reservation system
- Add date and time validation
- Implement store hours integration
- Add email notification system

## 2025-05-14: Authentication System and Component Reorganization

**Implemented:**

- Added authentication system

  - Created auth types and interfaces
  - Implemented auth slice for Redux store
  - Added login page component

- Reorganized reservation components

  - Moved components to new directory structure
  - Updated component imports and file organization
  - Improved ReservationDetail and ReservationList styling
  - Enhanced ReservationDetailPage and ReservationListPage UI

- Fixed UI issues

  - Fixed Steps component center alignment
  - Updated reservation types
  - Updated App.tsx routing

- Updated dependencies
  - Reinstalled Tailwind CSS packages with specific versions
  - Updated postcss configuration

**Technical Decisions:**

- Implemented authentication system for admin access
- Reorganized component structure for better maintainability
- Enhanced UI/UX with improved styling
- Fixed dependency issues for better stability

**Next Steps:**

- Temporarily postpone frontend tasks to focus on backend implementation
- Implement backend API endpoints and business logic
- Set up database and data access layer
- Implement authentication and authorization on backend
- Return to frontend tasks after backend completion:
  - Implement store hours management in admin dashboard
  - Add date and time validation
  - Implement store hours integration
  - Add email notification system
  - Login UI refactoring

## 2025-05-15: Backend Implementation - Domain Models and Repository Pattern

**Implemented:**

- Created domain models

  - Implemented Store model with business hours
  - Added Service model for store services
  - Created Reservation model with validation
  - Aligned models with frontend types

- Set up project structure

  - Created ASP.NET Core solution with multiple projects
  - Organized projects: API, Core, Infrastructure, Tests
  - Added setup script and guidelines
  - Updated README with project documentation

- Implemented repository pattern
  - Created generic repository interface
  - Added specific repositories for each entity
  - Implemented repository implementations
  - Added proper error handling

**Technical Decisions:**

- Used clean architecture principles
- Implemented repository pattern for data access
- Added proper validation and error handling
- Maintained type consistency with frontend
- Used Entity Framework Core for data access

**Next Steps:**

- Implement service layer with business logic
- Add API controllers with proper routing
- Implement authentication and authorization
- Add request/response DTOs
- Set up database migrations

## 2025-05-16: Backend Implementation - Error Handling and API Standardization

**Implemented:**

- Added global exception handling

  - Implemented GlobalExceptionMiddleware for centralized error handling
  - Created custom AppException class with error codes
  - Added standardized error response format
  - Implemented proper HTTP status code mapping

- Updated API controllers

  - Standardized error handling across all controllers
  - Updated StoreController with consistent response format
  - Updated ServiceController with consistent response format
  - Updated ReservationController with consistent response format

- Refactored domain models and infrastructure

  - Updated domain models with proper relationships
  - Updated ApplicationDbContext configurations
  - Refactored repository implementations
  - Updated service layer implementations

- Project maintenance
  - Removed obj directory from git tracking
  - Updated project files and dependencies
  - Updated API documentation
  - Reorganized repository interfaces

**Technical Decisions:**

- Implemented centralized error handling for consistent API responses
- Used custom exceptions for better error management
- Standardized HTTP status codes and error messages
- Maintained clean architecture principles
- Improved project structure and documentation

**Next Steps:**

- Implement authentication and authorization
- Add request/response DTOs
- Set up database migrations
- Add unit tests for services and controllers
- Implement email notification system

## 2025-05-17: Backend Implementation - API Response Standardization

**Implemented:**

- Added standardized API response format

  - Created ApiResponse<T> class for consistent response structure
  - Added ErrorResponse class for standardized error handling
  - Implemented success/error response wrappers
  - Added JSON property name attributes for consistent serialization

- Enhanced global exception handling

  - Integrated ApiResponse with GlobalExceptionMiddleware
  - Added detailed request logging (path, method)
  - Improved error message formatting
  - Added development environment specific error details

- Updated controllers with new response format

  - Wrapped all responses with ApiResponse<T>
  - Removed try-catch blocks in favor of global exception handling
  - Added detailed error messages for each operation
  - Standardized error response format across all controllers

- Improved service layer error handling
  - Replaced ArgumentException with KeyNotFoundException
  - Enhanced error messages for better clarity
  - Improved exception type selection
  - Added more descriptive error details

**Technical Decisions:**

- Implemented consistent API response format for better frontend integration
- Enhanced error handling with detailed logging
- Standardized error messages and response structure
- Improved exception handling in service layer
- Maintained clean architecture principles

**Next Steps:**

1. Frontend-Backend Integration

   - Connect frontend API services with new standardized responses
   - Update frontend error handling to match new response format
   - Test all API endpoints with frontend
   - Implement proper loading states and error displays

2. Database Setup (After Frontend Integration)

   - Set up actual database connection
   - Configure connection strings
   - Run initial migrations
   - Test with real data

3. Future Enhancements (Post-MVP)

   - Implement authentication system
   - Add email notification system
   - Add admin dashboard features
   - Implement store hours management

## 2025-05-18: Backend Refactoring - DTO, AutoMapper, and Controller Standardization

**Implemented:**

- Added Create/Response DTOs for Reservation, Service, and Store domains
  - Each domain now has its own Create and Response DTOs for clear API contracts
- Introduced AutoMapper MappingProfile for domain <-> DTO conversion
  - Centralized mapping logic for all domain models and DTOs
- Refactored ReservationController to use DTOs and AutoMapper
  - All endpoints now use CreateReservationDto and ReservationResponseDto
  - Model <-> DTO conversion is handled by AutoMapper
  - Improved response consistency and type safety
- Refactored ServiceController to use DTOs and AutoMapper
  - All endpoints now use CreateServiceDto and ServiceResponseDto
  - Model <-> DTO conversion is handled by AutoMapper
- Refactored StoreController to use DTOs and AutoMapper
  - All endpoints now use CreateStoreDto and StoreResponseDto
  - Model <-> DTO conversion is handled by AutoMapper
- Added extension methods for model-DTO conversion (where needed)
- Updated project files and DI registration for AutoMapper
- Updated infrastructure, middleware, and configuration files to support new structure
  - Updated exception middleware, DI registration, and project files
  - Refactored StoreHour model and related infrastructure

**Technical Decisions:**

- Adopted DTO pattern for all API input/output to ensure clear contract between frontend and backend
- Used AutoMapper to centralize and automate mapping logic, reducing boilerplate and potential bugs
- Improved maintainability and scalability by separating API contracts from domain models
- Ensured all API responses are consistent and type-safe
- Updated infrastructure and middleware to support new API structure

**Next Steps:**

- Test all endpoints with frontend integration
- Add/Update unit tests for controller and service layers
- Document new API contracts and update API documentation

## 2025-05-19: Database Migration and Seed Data Implementation

**Implemented:**

- Migrated from SQL Server to SQLite

  - Updated database configuration in Program.cs
  - Modified connection string in appsettings.json
  - Added SQLite package dependencies
  - Created database migration guides

- Added initial migrations

  - Created migration files for all entities
  - Configured proper relationships between entities
  - Added database command guide
  - Set up migration assembly configuration

- Updated domain models

  - Made StoreHour nullable in Store model
  - Added StoreHourId to RegularHour
  - Added documentation for decimal type usage
  - Fixed model relationships for SQLite

- Implemented seed data

  - Added seed data for Store entities (3 locations)
  - Added seed data for StoreHour entities
  - Added seed data for RegularHour entities (all days for each store)
  - Added seed data for Service entities (Haircut and Hair Coloring)

- Updated frontend integration
  - Simplified reservation data structure
  - Updated reservation hooks and store
  - Removed unnecessary data wrapping
  - Fixed API integration issues

**Technical Decisions:**

- Switched to SQLite for better cross-platform compatibility
- Used explicit IDs in seed data to prevent conflicts
- Implemented proper entity relationships
- Maintained data consistency across all entities
- Simplified frontend data handling

**Next Steps:**

- Implement store hours validation
- Add reservation time slot validation
- Implement email notification system
- Add admin dashboard features
- Create unit tests for new functionality

## 2025-05-27: Authentication System Implementation and Database Migration

**Implemented:**

- Added authentication system
  - Implemented JWT-based authentication
  - Created User model and database table
  - Added AuthService and AuthController
  - Configured JWT settings in Program.cs

- Updated database configuration
  - Moved database connection string to .env file
  - Added DotNetEnv package for environment variable management
  - Recreated database migrations with User table
  - Fixed database connection issues

- Enhanced security
  - Added password hashing with BCrypt
  - Implemented JWT token validation
  - Added role-based authorization
  - Secured sensitive configuration in .env

**Technical Decisions:**

- Used JWT for stateless authentication
- Implemented secure password hashing
- Moved sensitive data to environment variables
- Maintained clean architecture principles
- Improved database configuration

**Next Steps:**

- Implement frontend authentication integration
- Add user management features
- Implement password reset functionality
- Add email verification system
- Create admin dashboard for user management

## 2025-05-19 ~ 2025-05-27: Store-Service Many-to-Many Relationship Implementation

**Implemented:**

- Implemented many-to-many relationship between Store and Service
  - Created StoreService model for relationship management
  - Updated Store and Service models with navigation properties
  - Configured entity relationships in ApplicationDbContext
  - Added cascade delete behavior for Store-StoreService relationship
  - Added restrict delete behavior for Service-StoreService relationship

- Updated database schema
  - Recreated database migrations
  - Added StoreServices table with composite key
  - Configured proper foreign key constraints
  - Added seed data for Store-Service relationships

- Refactored service layer
  - Updated ServiceService to handle StoreService relationships
  - Modified GetServicesByStoreIdAsync to use StoreServices relationship
  - Removed direct StoreId reference from Service model
  - Improved relationship management in CreateServiceAsync

**Technical Decisions:**

- Used junction table (StoreService) for many-to-many relationship
- Implemented proper cascade/restrict delete behaviors
- Removed redundant StoreId from Service model
- Used Entity Framework's automatic relationship management
- Maintained data integrity with proper constraints

**Next Steps:**

- Test all service endpoints with new relationship structure
- Update frontend to handle new relationship model
- Add service management features in admin dashboard
- Implement service availability checking
- Add service scheduling features

## 2025-05-28: DTO Implementation, Error Handling, and Environment Configuration

**Implemented:**

- Enhanced DTO structure and validation
  - Added UpdateReservationDto and UpdateStoreDto
  - Added required field validations
  - Improved JSON property naming
  - Added proper type constraints and default values

- Updated service interfaces and implementation
  - Modified service interfaces to accept and return DTOs instead of entities
  - Added new methods for specific operations (e.g., ConfirmReservation)
  - Updated method signatures to reflect DTO usage
  - Improved type safety with explicit DTO types
  - Added AutoMapper integration for entity-DTO conversion
  - Implemented proper error handling with specific exceptions
  - Added business logic validation
  - Improved error messages and exception types

- Updated controllers and error handling
  - Removed entity mapping from controllers
  - Implemented consistent response handling
  - Added proper HTTP status codes
  - Improved error handling and response structure
  - Added new error codes for better error categorization
  - Added BadRequest error code for validation errors
  - Improved error code organization
  - Added business rule violation codes

- Enhanced environment and security configuration
  - Added .env file to gitignore
  - Added environment variable handling
  - Updated configuration for secure credential management
  - Added proper environment file patterns
  - Improved security with environment variable management

**Technical Decisions:**

- Adopted DTO pattern for all API operations
- Used AutoMapper for consistent entity-DTO conversion
- Implemented comprehensive error handling system
- Enhanced type safety with explicit DTOs
- Improved security with environment variable management
- Maintained clean architecture principles

**Next Steps:**

- Test all endpoints with new DTO structure
- Update frontend to handle new DTO format
- Add validation middleware for DTOs
- Implement comprehensive error handling in frontend
- Add proper error display in UI components
- Document new API contracts and update API documentation

## 2025-05-29 ~ 2025-05-31: Frontend-Backend Integration and UI Improvements

**Implemented:**

- Enhanced store hours management
  - Improved data structure for store hours
  - Updated store hours management in admin dashboard
  - Integrated store hours with backend API
  - Added proper data synchronization

- Improved UI/UX
  - Added loading spinner component
  - Enhanced admin dashboard layout
  - Updated data format to match backend structure
  - Improved user feedback during loading states

- Refactored authentication system
  - Moved login dispatch logic to custom hooks
  - Updated dashboard URL structure
  - Improved authentication flow
  - Enhanced code organization

- Backend improvements
  - Updated GetAllStores route to include store hours
  - Enhanced repository layer for store hours
  - Improved data consistency
  - Optimized API responses

**Technical Decisions:**

- Separated authentication logic into custom hooks for better reusability
- Implemented loading states for better user experience
- Aligned frontend data structure with backend models
- Enhanced store hours management system
- Improved code organization and maintainability

**Next Steps:**

- Implement store hours validation
- Add reservation time slot validation
- Enhance error handling in UI components
- Add more admin dashboard features
- Implement email notification system

## 2025-06-01: Special Dates Management and Integration Planning

**Planned Implementation:**

- Special Dates Management
  - Add special dates management UI in admin dashboard
  - Implement frontend logic for adding/editing special dates
  - Create batch save functionality for store hours changes
  - Integrate with backend API for data persistence

- Google Calendar and Email Integration
  - Implement Google Calendar API integration
  - Add email notification system using Google API
  - Create reservation confirmation emails
  - Add calendar event creation for reservations
  - Implement email templates for different notification types

**Technical Decisions:**

- Use Google Calendar API for event management
- Implement Google Gmail API for email notifications
- Create reusable email templates
- Implement batch processing for store hours updates
- Use proper error handling for API integrations

**Next Steps:**

1. Special Dates Management
   - Design and implement special dates UI components
   - Create batch save functionality
   - Implement validation for special dates
   - Add error handling for save operations

2. Google API Integration
   - Set up Google Cloud Project
   - Configure OAuth 2.0 authentication
   - Implement Calendar API integration
   - Add Gmail API integration
   - Create email templates
   - Implement notification system

3. Testing and Documentation
   - Test special dates management
   - Verify Google API integrations
   - Update API documentation
   - Add integration guides

## 2025-06-02 ~ 2025-06-04: Reservation Detail Page Implementation and Data Handling

**Implemented:**

- Enhanced reservation detail page
  - Implemented API integration for fetching reservation details
  - Added loading and error states
  - Improved UI styling and layout
  - Added back navigation functionality

- Updated reservation data handling
  - Fixed agreedToTerms property mapping in DTO
  - Added AgreedToTerms configuration in ApplicationDbContext
  - Updated default reservation status to Confirmed
  - Improved data consistency between frontend and backend

- Improved component structure
  - Updated ReservationDetail component styling
  - Enhanced ReservationForm navigation flow
  - Improved StoreSelection component type handling
  - Added proper error handling in components

- Enhanced API integration
  - Added getReservationById endpoint
  - Updated reservation service with proper API endpoints
  - Modified useReservation hook for better error handling
  - Updated reservation slice to use service methods

**Technical Decisions:**

- Used proper JSON property naming for consistent data handling
- Implemented proper loading states for better UX
- Enhanced error handling across components
- Improved type safety with proper TypeScript types
- Maintained clean architecture principles

**Next Steps:**

1. Reservation Management Enhancement
   - Refactor reservation detail page for better UX
   - Implement reservation cancellation feature
   - Add reservation modification flow (cancel and rebook)
   - Improve error handling and user feedback

2. Google Calendar Integration
   - Set up Google Calendar API integration
   - Implement calendar event creation for reservations
   - Add calendar event updates for modifications
   - Handle calendar event deletion for cancellations

3. Email Service Implementation
   - Set up email notification system
   - Create email templates for different scenarios
   - Implement email sending for reservations
   - Add email notifications for modifications and cancellations

4. Testing and Documentation
   - Test all new features
   - Update API documentation
   - Add integration guides
   - Create user guides for new features
