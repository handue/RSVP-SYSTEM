# RSVP System - Frontend

The frontend application for the RSVP System, built with React, TypeScript, and Redux Toolkit.

## Technology Stack

- React 18
- TypeScript
- Redux Toolkit
- React Router
- Tailwind CSS
- React Datepicker
- Lucide React (icons)
- Axios for API requests
- Vite (build tool)

## Project Structure

```
src/
├── components/          # Reusable UI components
│   ├── ui/              # Basic UI elements
│   ├── reservation/     # Reservation specific components
│   └── store-hours/     # Store hours management components
├── pages/               # Page components for different routes
│   ├── reservation/     # Reservation feature with Redux slice
│   └── admin/           # Admin dashboard and reservation management
├── hooks/               # Custom hooks
├── services/            # API service functions
├── store/               # Redux Toolkit setup
├── types/               # TypeScript type definitions
└── utils/               # Utility functions
```

## Getting Started

### Prerequisites

- Node.js 16+ and npm

### Installation

1. Clone the repository and navigate to the client directory
```bash
cd client
```

2. Install dependencies
```bash
npm install
```

3. Create a `.env` file in the client directory with the following variables
```
VITE_API_URL=http://localhost:5173/api
```

4. Start the development server
```bash
npm run dev
```

The application will be available at `http://localhost:5173`.

## Available Scripts

- `npm run dev` - Start the development server
- `npm run build` - Build the app for production
- `npm run preview` - Preview the production build locally
- `npm run lint` - Run ESLint
- `npm run type-check` - Run TypeScript type checking

## Features

### User Reservation Flow
- Store selection
- Service type selection
- Date and time slot selection
- Form validation
- Confirmation system

### Admin Management
- Store hours configuration
- Special dates management
- Regular hours scheduling

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

## Contributing

1. Create a feature branch from `main`
2. Make your changes
3. Commit with meaningful commit messages following the semantic format
4. Push and create a pull request

## Building for Production

```bash
npm run build
```

The build artifacts will be stored in the `dist/` directory.