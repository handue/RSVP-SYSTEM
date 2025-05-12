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

## 2025-05-12: Frontend Architecture

**Decisions made:**
- Using feature-based folder structure
- Implementing custom hooks for business logic
- Using Tailwind CSS for styling

**Technical challenges:**
- Solved date manipulation challenges using date-fns library
- Implemented time slot filtering based on store hours

**Next steps:**
- Implement UI components
- Connect Redux store