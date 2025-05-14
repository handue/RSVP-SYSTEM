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
- `server`: Backend code (future)
- Empty: affects multiple parts

### Examples:

feat(client): add reservation form component
fix(client): resolve date picker validation issue
docs: update project README
chore: update dependencies

## Project Organization

- `/client`: Frontend React application
  - `/src/components/ui`: Reusable UI components
  - `/src/pages`: Page components
  - `/src/types`: TypeScript type definitions
  - `/src/services`: API services
  - `/src/store`: Redux store configuration
- `/spring-server`: Backend Spring application (planned)

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

## API Configuration Guidelines

- Place API configuration in services directory
- Use environment variables for base URL
- Configure CORS and credentials properly
- Implement proper error handling
- Use interceptors for common request/response handling

## Redux Slice Guidelines

- Use createAsyncThunk for API calls
- Implement proper error handling
- Add loading states for better UX
- Use TypeScript for type safety
- Keep slices focused and single-purpose
- Add state reset functionality when needed

## Custom Hooks Guidelines

- Create typed hooks for Redux store access
- Implement proper error handling in hooks
- Keep hooks focused and single-purpose
- Use TypeScript for type safety
- Document complex hook logic