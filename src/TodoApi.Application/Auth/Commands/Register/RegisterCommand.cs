using MediatR;

namespace TodoApi.Application.Auth.Commands.Register;

public record RegisterCommand(string Username, string Email, string Password)
    : IRequest<string>; // Returns JWT token
