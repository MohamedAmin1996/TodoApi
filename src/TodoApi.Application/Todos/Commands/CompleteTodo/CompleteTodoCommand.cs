using MediatR;
using TodoApi.Application.Todos.DTOs;

namespace TodoApi.Application.Todos.Commands.CompleteTodo;

public record CompleteTodoCommand(Guid Id, Guid UserId) : IRequest<TodoResponse>;
