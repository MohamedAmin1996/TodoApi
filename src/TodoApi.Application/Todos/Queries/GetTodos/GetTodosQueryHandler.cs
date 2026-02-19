using AutoMapper;
using MediatR;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Todos.DTOs;

namespace TodoApi.Application.Todos.Queries.GetTodos;

public sealed class GetTodosQueryHandler
    : IRequestHandler<GetTodosQuery, IEnumerable<TodoResponse>>
{
    private readonly ITodoRepository _repo;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(ITodoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoResponse>> Handle(GetTodosQuery q, CancellationToken ct)
    {
        var todos = await _repo.GetAllByUserAsync(q.UserId, q.Page, q.PageSize, ct);
        return _mapper.Map<IEnumerable<TodoResponse>>(todos);
    }
}
