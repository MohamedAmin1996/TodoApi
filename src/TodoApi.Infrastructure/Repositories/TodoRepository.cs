using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enums;
using TodoApi.Infrastructure.Data;

namespace TodoApi.Infrastructure.Repositories;

// Implements ITodoRepository — the contract defined in Application.
// Application has no idea EF Core or Npgsql exist.
public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _ctx;
    public TodoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<TodoItem?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default)
        => await _ctx.Todos
            .Where(t => t.Id == id && t.UserId == userId && t.Status != TodoStatus.Deleted)
            .FirstOrDefaultAsync(ct);

    public async Task<IEnumerable<TodoItem>> GetAllByUserAsync(
        Guid userId, int page, int pageSize, CancellationToken ct = default)
        => await _ctx.Todos
            .Where(t => t.UserId == userId && t.Status != TodoStatus.Deleted)
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

    public async Task AddAsync(TodoItem todo, CancellationToken ct = default)
        => await _ctx.Todos.AddAsync(todo, ct);

    public Task UpdateAsync(TodoItem todo, CancellationToken ct = default)
    {
        _ctx.Todos.Update(todo);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Guid id, Guid userId, CancellationToken ct = default)
        => await _ctx.Todos.AnyAsync(t => t.Id == id && t.UserId == userId, ct);
}
