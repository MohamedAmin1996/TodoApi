using MediatR;

namespace TodoApi.Application.Todos.Commands.DeleteTodo;

// Returns Unit (nothing) — a delete has no meaningful response body.
public record DeleteTodoCommand(Guid Id, Guid UserId) : IRequest;
