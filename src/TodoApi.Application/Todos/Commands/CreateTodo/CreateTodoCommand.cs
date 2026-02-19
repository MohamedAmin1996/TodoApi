using MediatR;
using TodoApi.Application.Todos.DTOs;

namespace TodoApi.Application.Todos.Commands.CreateTodo;

public record CreateTodoCommand(
    string Title,
    string? Description,
    DateTime? DueDate,
    Guid UserId
) : IRequest<TodoResponse>;
