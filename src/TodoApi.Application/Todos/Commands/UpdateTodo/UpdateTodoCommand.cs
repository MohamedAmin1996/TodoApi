using MediatR;
using TodoApi.Application.Todos.DTOs;

namespace TodoApi.Application.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    Guid UserId
) : IRequest<TodoResponse>;
