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
- `/spring-server`: Backend Spring application (planned)

## Development Process

1. Create GitHub issue for the task
2. Implement changes
3. Test thoroughly
4. Commit with descriptive message
5. Update documentation if necessary
