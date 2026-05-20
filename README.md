# Task Manager

A small task management app: create tasks, list them, and toggle completion.

- **Backend**: .NET 10, ASP.NET Core Web API, EF Core (in-memory) - Clean Architecture
- **Frontend**: React 18 (no build step - Babel standalone via CDN)

---

## Prerequisites

- **.NET 10 SDK**
- **Python 3** - only used to serve the static frontend on port `5173`

---

## Project structure

```
backend/
  src/
    TaskManager.Domain/          # Entities, business rules (no dependencies)
    TaskManager.Application/     # DTOs, service interfaces, use cases
    TaskManager.Infrastructure/  # EF Core DbContext + repository
    TaskManager.Api/             # Controllers, Program.cs, CORS, DI
  tests/
    TaskManager.Tests/           # xUnit tests

frontend/
  index.html
  src/
    api.js                       # fetch wrapper for the API
    main.jsx                     # React entry point
    styles.css
    components/
      App.jsx                    # State + data flow
      AddTaskForm.jsx
      TaskList.jsx
      TaskItem.jsx
```


## Running the app

Open **two terminals**.

### 1. Backend (port 5108)

```bash
cd backend
dotnet run --project src/TaskManager.Api
```

The API listens on `http://localhost:5108`. Endpoints:

| Method | Path                          | Purpose                       |
| ------ | ----------------------------- | ----------------------------- |
| GET    | `/api/tasks`                  | List all tasks (newest first) |
| POST   | `/api/tasks`                  | Create a task                 |
| PATCH  | `/api/tasks/{id}/toggle`      | Toggle completion             |
| DELETE | `/api/tasks/{id}`             | Delete a task                 |

OpenAPI document is exposed at `/openapi/v1.json` in Development.

### 2. Frontend (port 5173)

```bash
cd frontend
python -m http.server 5173
```

Then open <http://localhost:5173>. The port `5173` is hard-coded in the API's CORS policy.

---

## Running the tests

```bash
cd backend
dotnet test
```

Covers the `TaskItem` domain entity and `TaskService` orchestration with a fake repository.

---
