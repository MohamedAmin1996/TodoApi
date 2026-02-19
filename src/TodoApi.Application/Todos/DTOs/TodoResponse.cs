using TodoApi.Domain.Enums;

namespace TodoApi.Application.Todos.DTOs;

public record TodoResponse(
    Guid Id,
    string Title,
    string? Description,
    TodoStatus Status,
    DateTime? DueDate,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
