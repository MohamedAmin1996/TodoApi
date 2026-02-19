using MediatR;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Todos.DTOs;
using TodoApi.Domain.Entities;
using AutoMapper;

namespace TodoApi.Application.Todos.Commands.CreateTodo;

public sealed class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoResponse>
{
    private readonly ITodoRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;


    public CreateTodoCommandHandler(ITodoRepository repo, IUnitOfWork uow, IMapper mapper)
    {
        _repo = repo; _uow = uow; _mapper = mapper;
    }


    public async Task<TodoResponse> Handle(CreateTodoCommand cmd, CancellationToken ct)
    {
        var todo = TodoItem.Create(cmd.Title, cmd.Description, cmd.DueDate, cmd.UserId);

        await _repo.AddAsync(todo, ct);
        await _uow.SaveChangesAsync(ct);

        return _mapper.Map<TodoResponse>(todo);
    }
}
