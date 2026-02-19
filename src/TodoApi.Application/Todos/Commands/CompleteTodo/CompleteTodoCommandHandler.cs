using AutoMapper;
using MediatR;
using TodoApi.Application.Common.Exceptions;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Todos.DTOs;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Todos.Commands.CompleteTodo;

public sealed class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, TodoResponse>
{
    private readonly ITodoRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CompleteTodoCommandHandler(ITodoRepository repo, IUnitOfWork uow, IMapper mapper)
    {
        _repo = repo; _uow = uow; _mapper = mapper;
    }

    public async Task<TodoResponse> Handle(CompleteTodoCommand cmd, CancellationToken ct)
    {
        var todo = await _repo.GetByIdAsync(cmd.Id, cmd.UserId, ct)
            ?? throw new NotFoundException(nameof(TodoItem), cmd.Id);

        // Complete() throws BusinessRuleException if already completed.
        // That exception is caught by ExceptionHandlingMiddleware → 422.
        todo.Complete();

        await _repo.UpdateAsync(todo, ct);
        await _uow.SaveChangesAsync(ct);

        return _mapper.Map<TodoResponse>(todo);
    }
}
