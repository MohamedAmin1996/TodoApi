using MediatR;
using TodoApi.Application.Todos.DTOs;

namespace TodoApi.Application.Todos.Queries.GetTodoById;

// UserId is included so a user can never fetch another user's todo.
// The repository WHERE clause enforces this at the DB level.
public record GetTodoByIdQuery(Guid Id, Guid UserId) : IRequest<TodoResponse>;
