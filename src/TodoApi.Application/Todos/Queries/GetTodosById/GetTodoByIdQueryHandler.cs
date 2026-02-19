using AutoMapper;
using MediatR;
using TodoApi.Application.Common.Exceptions;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Todos.DTOs;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Todos.Queries.GetTodoById;

public sealed class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoResponse>
{
    private readonly ITodoRepository _repo;
    private readonly IMapper _mapper;

    public GetTodoByIdQueryHandler(ITodoRepository repo, IMapper mapper)
    {
        _repo = repo; _mapper = mapper;
    }

    public async Task<TodoResponse> Handle(GetTodoByIdQuery q, CancellationToken ct)
    {
        var todo = await _repo.GetByIdAsync(q.Id, q.UserId, ct)
            ?? throw new NotFoundException(nameof(TodoItem), q.Id);

        return _mapper.Map<TodoResponse>(todo);
    }
}
