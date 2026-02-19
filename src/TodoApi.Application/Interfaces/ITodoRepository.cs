using TodoApi.Domain.Entities;


namespace TodoApi.Application.Interfaces;

public interface ITodoRepository
{
    Task<TodoItem?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);
    Task<IEnumerable<TodoItem>> GetAllByUserAsync(Guid userId, int page, int pageSize, CancellationToken ct = default);
    Task AddAsync(TodoItem todo, CancellationToken ct = default);
    Task UpdateAsync(TodoItem todo, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, Guid userId, CancellationToken ct = default);
}
