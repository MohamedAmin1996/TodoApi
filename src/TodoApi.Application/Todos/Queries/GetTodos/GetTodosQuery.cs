using MediatR;
using TodoApi.Application.Todos.DTOs;

namespace TodoApi.Application.Todos.Queries.GetTodos;

public record GetTodosQuery(Guid UserId, int Page = 1, int PageSize = 20)
    : IRequest<IEnumerable<TodoResponse>>;
