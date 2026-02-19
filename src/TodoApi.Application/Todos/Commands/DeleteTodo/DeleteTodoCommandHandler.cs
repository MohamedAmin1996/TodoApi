using MediatR;
using TodoApi.Application.Common.Exceptions;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Todos.Commands.DeleteTodo;

public sealed class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly ITodoRepository _repo;
    private readonly IUnitOfWork _uow;


    public DeleteTodoCommandHandler(ITodoRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task Handle(DeleteTodoCommand cmd, CancellationToken ct)
    {
        var todo = await _repo.GetByIdAsync(cmd.Id, cmd.UserId, ct)
            ?? throw new NotFoundException(nameof(TodoItem), cmd.Id);

        // Domain method sets Status = Deleted — no row is removed from the DB
        todo.Delete();

        await _repo.UpdateAsync(todo, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
