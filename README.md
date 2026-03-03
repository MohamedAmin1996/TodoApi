# TodoApi


A production-grade REST API built with ASP.NET Core 8, demonstrating
Clean Architecture, CQRS, and SOLID principles.


[![CI](https://github.com/MohamedAmin1996/TodoApi/actions/workflows/ci.yml/badge.svg)](https://github.com/MohamedAmin1996/TodoApi/actions/workflows/ci.yml)
[![Docker Pulls](https://img.shields.io/docker/pulls/mohamedamin1996/todoapi)](https://hub.docker.com/r/mohamedamin1996/todoapi)


## Architecture


Clean Architecture with four layers enforced by project references:


- **Domain** — Entities, business rules, no external dependencies
- **Application** — CQRS handlers (MediatR), interfaces, validation (FluentValidation)
- **Infrastructure** — EF Core, Npgsql, BCrypt, JWT implementation
- **API** — Controllers, middleware, Swagger, rate limiting, Serilog


## Tech Stack


| Concern | Technology |
|---|---|
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core + Npgsql (PostgreSQL) |
| CQRS | MediatR |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| Auth | JWT Bearer |
| Logging | Serilog |
| Containerization | Docker + Docker Compose |
| CI/CD | GitHub Actions |
| Testing | xUnit + Moq + FluentAssertions |


## Endpoints


| Method | Route | Auth | Description |
|---|---|---|---|
| POST | /api/v1/auth/register | No | Register a new user |
| POST | /api/v1/auth/login | No | Login, receive JWT |
| GET | /api/v1/todos | Yes | Get paginated todos |
| POST | /api/v1/todos | Yes | Create a todo |
| PUT | /api/v1/todos/{id} | Yes | Update a todo |
| POST | /api/v1/todos/{id}/complete | Yes | Mark as complete |
| DELETE | /api/v1/todos/{id} | Yes | Soft delete a todo |
| GET | /health | No | Health check |


## Running Locally


### Prerequisites
- Docker Desktop


### With Docker Compose
```bash
git clone https://github.com/MohamedAmin1996/TodoApi.git
cd TodoApi
docker-compose up --build
```
API: http://localhost:8080
Swagger: http://localhost:8080/swagger


### Without Docker
1. Install .NET 8 SDK
2. Install PostgreSQL
3. Update connection string in appsettings.Development.json
4. Run migrations: `dotnet ef database update --project src/TodoApi.Infrastructure --startup-project src/TodoApi.API`
5. `dotnet run --project src/TodoApi.API`


## Testing
```bash
dotnet test
```


## SOLID Principles Applied


| Principle | Where |
|---|---|
| Single Responsibility | Each handler handles exactly one use case |
| Open/Closed | Pipeline behaviors add cross-cutting concerns without touching handlers |
| Liskov Substitution | BcryptPasswordHasher fulfils IPasswordHasher fully |
| Interface Segregation | ITodoRepository, IUnitOfWork, IPasswordHasher are separate |
| Dependency Inversion | Handlers depend on interfaces; Infrastructure is never referenced from Application |
