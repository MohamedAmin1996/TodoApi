namespace TodoApi.Application.Auth.DTOs;

public record UserResponse(
    Guid Id,
    string Username,
    string Email,
    DateTime CreatedAt
);
