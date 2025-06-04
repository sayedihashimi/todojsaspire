# TodojsAspire

A full-stack Todo application built with React (frontend), .NET Aspire (backend), and Vite for rapid frontend development. This repository demonstrates a modern, modular approach to building and running a multi-project solution with both web and API services.

## Project Structure

```
todojsaspire/
├── LICENSE
├── src/
│   ├── package.json
│   ├── TodojsAspire.sln
│   ├── todo-frontend/           # React + Vite frontend
│   │   ├── README.md
│   │   ├── package.json
│   │   ├── vite.config.js
│   │   └── ...
│   ├── TodojsAspire.ApiService/ # .NET Aspire API service
│   │   ├── Program.cs
│   │   ├── Todo.cs
│   │   ├── TodoDbContext.cs
│   │   ├── TodoEndpoints.cs
│   │   └── ...
│   ├── TodojsAspire.AppHost/    # .NET Aspire AppHost
│   │   ├── AppHost.cs
│   │   └── ...
│   ├── TodojsAspire.ServiceDefaults/ # Shared service defaults
│   │   └── ...
```

## Solution Overview

- **Frontend:**
  - Located in `src/todo-frontend/`
  - Built with React and Vite
  - Handles all user interactions and communicates with the backend API
  - See [`src/todo-frontend/README.md`](src/todo-frontend/README.md) for details

- **Backend:**
  - .NET Aspire solution with multiple projects:
    - `TodojsAspire.ApiService/`: Minimal API for managing todos (CRUD, reorder, etc.)
    - `TodojsAspire.AppHost/`: AppHost for orchestrating services
    - `TodojsAspire.ServiceDefaults/`: Shared configuration and extensions

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18+ recommended)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)

### 1. Clone the Repository
```sh
git clone <your-repo-url>
cd todojsaspire
```

### 2. Run the Backend (.NET Aspire)
```sh
cd src
# Restore and build the solution
 dotnet restore
 dotnet build
# Run the AppHost (or run ApiService directly for development)
dotnet run --project TodojsAspire.AppHost
```

### 3. Run the Frontend (React + Vite)
```sh
cd src/todo-frontend
npm install
npm run dev
# or
yarn install
yarn dev
```

The frontend will be available at [http://localhost:5173](http://localhost:5173) and will proxy API requests to the backend.

## API Endpoints

The backend exposes the following endpoints (see `TodojsAspire.ApiService`):
- `GET /api/Todo` — fetch all todos
- `POST /api/Todo` — add a new todo
- `DELETE /api/Todo/{id}` — delete a todo
- `POST /api/Todo/move-up/{id}` — move a todo up
- `POST /api/Todo/move-down/{id}` — move a todo down

## Development Notes
- The solution uses Entity Framework Core for data access (see `TodoDbContext.cs`).
- Migrations are stored in `src/TodojsAspire.ApiService/Migrations/`.
- The frontend expects the backend API to be available at `/api/Todo` (see Vite proxy config if needed).

## License

This project is licensed under the terms of the [MIT License](LICENSE).
