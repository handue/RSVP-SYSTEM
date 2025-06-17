# Development Guidelines

This document outlines the workflow and best practices for this project.

## Git Workflow

This project uses a simplified Git workflow:

- The `main` branch is the primary branch for development
- Code is committed directly to `main` for most changes
- Feature branches are used only for complex features that take multiple days

## Commit Message Format

<type>(<scope>): <subject>
[optional body]

### Types:

- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, etc.)
- `refactor`: Code refactoring
- `perf`: Performance improvements
- `test`: Adding or updating tests
- `chore`: Updating build tasks, etc.

### Scope:

- `client`: Frontend code
- `server`: Backend code
- `auth`: Authentication related
- `email`: Email service related
- `calendar`: Google Calendar integration
- `db`: Database related
- Empty: affects multiple parts

### Examples:

feat(client): add reservation form component
fix(client): resolve date picker validation issue
feat(server): implement Google Calendar integration
feat(email): add reservation confirmation emails
feat(db): add store and service name columns
docs: update API documentation with Swagger
chore: update dependencies

## Project Organization

- `/client`: Frontend React application
  - `/src/components/ui`: Reusable UI components
  - `/src/components/reservation`: Reservation related components
  - `/src/pages`: Page components
  - `/src/types`: TypeScript type definitions
  - `/src/services`: API services
  - `/src/store`: Redux store configuration
  - `/src/hooks`: Custom React hooks
  - `/src/middleware`: Authentication middleware
  - `/src/utils`: Utility functions
  - `/src/lib`: Third-party library configurations
  - `/src/styles`: Global styles and CSS
  - `/src/assets`: Static assets

- `/server-ASP.NET`: Backend ASP.NET Core application
  - `/RSVP.API`: API controllers and endpoints
  - `/RSVP.Core`: Domain models and interfaces
  - `/RSVP.Infrastructure`: Data access and external services
  - `/RSVP.Tests`: Unit and integration tests
  - `/GoogleAuthTool`: Google API authentication tools

## Development Process

1. Create GitHub issue for the task
2. Implement changes
3. Test thoroughly
4. Commit with descriptive message
5. Update documentation if necessary

## TypeScript Guidelines

- All new code must be written in TypeScript
- Use interfaces for type definitions
- Keep type definitions in the types directory
- Follow naming conventions: PascalCase for interfaces
- Document complex type structures
- Use proper type guards and assertions

## API Configuration Guidelines

- Place API configuration in services directory
- Use environment variables for base URL
- Configure CORS and credentials properly
- Implement proper error handling
- Use interceptors for common request/response handling
- Document APIs using Swagger/OpenAPI
- Handle token refresh and authentication

## Redux Slice Guidelines

- Use createAsyncThunk for API calls
- Implement proper error handling
- Add loading states for better UX
- Use TypeScript for type safety
- Keep slices focused and single-purpose
- Add state reset functionality when needed
- Handle API response data properly

## Custom Hooks Guidelines

- Create typed hooks for Redux store access
- Implement proper error handling in hooks
- Keep hooks focused and single-purpose
- Use TypeScript for type safety
- Document complex hook logic
- Handle loading and error states
- Implement proper cleanup in useEffect

## External Service Integration Guidelines

- Use environment variables for API keys
- Implement proper error handling
- Add retry logic for failed requests
- Handle token refresh when needed
- Document integration points
- Add proper logging for debugging
- Implement proper error recovery

### Google API Integration Guidelines

- Set up Google Cloud Project and enable required APIs
- Configure OAuth 2.0 credentials
- Store credentials securely in environment variables
- Implement proper token management
- Handle API rate limits
- Add proper error handling for API failures
- Implement retry mechanism for failed requests
- Add logging for API operations
- Handle token refresh automatically
- Implement proper cleanup for resources

## Database Guidelines

- Use migrations for schema changes
- Follow naming conventions for tables and columns
- Implement proper relationships
- Use appropriate data types
- Add proper indexes
- Handle data consistency
- Implement proper error handling

## Security Guidelines

- Use environment variables for sensitive data
- Implement proper authentication
- Handle authorization properly
- Use HTTPS for all communications (only in production)
- Implement proper input validation
- Handle sensitive data properly
- Follow security best practices
- Secure API keys and credentials
- Implement proper token management
- Add rate limiting for API endpoints
- Use secure password hashing
- Implement proper session management
- Add request validation
- Use secure headers
- Implement CORS properly

## Documentation Guidelines

- Keep API documentation up to date with Swagger
- Document all environment variables
- Maintain setup guidelines
- Keep README.md updated
- Document external service configurations
- Maintain API documentation in APidocs-standard-guide.txt