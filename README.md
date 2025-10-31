# Task Manager (Backend + Frontend)

This repo contains a simple Task Manager:
- Backend: .NET 8 Web API (in-memory)
- Frontend: React + TypeScript (Vite)

Ports:
- Backend API: http://localhost:5000
- Frontend dev: http://localhost:5173

## Requirements
- .NET 8 SDK
- Node 18+ / npm or yarn

## Run Backend
1. Open a terminal in the `backend` folder.
2. Run:
   dotnet run
3. API will be available at http://localhost:5000. Swagger UI at http://localhost:5000/swagger when in Development.

Endpoints:
- GET /api/tasks
- POST /api/tasks
- PUT /api/tasks/{id}
- DELETE /api/tasks/{id}

## Run Frontend
1. Open a terminal in the `frontend` folder.
2. Install:
   npm install
3. Start dev server:
   npm run dev
4. Open http://localhost:5173

## Notes
- The backend uses an in-memory store; restarting it resets tasks.
- CORS is enabled for local dev. Tighten this in production.
