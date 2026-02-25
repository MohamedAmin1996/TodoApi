namespace TodoApi.API.DTOs;


// HTTP input shape — belongs to API, not Application.
// Controller maps this to CreateTodoCommand before calling MediatR.
public record CreateTodoRequest(
    string Title,
    string? Description,
    DateTime? DueDate
);
