using MediatR;
using TodoApi.Application.Common.Exceptions;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Auth.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenService _tokens;
    private readonly IUnitOfWork _uow;

    public RegisterCommandHandler(
        IUserRepository users, IPasswordHasher hasher,
        ITokenService tokens, IUnitOfWork uow)
    {
        _users = users; _hasher = hasher; _tokens = tokens; _uow = uow;
    }

    public async Task<string> Handle(RegisterCommand cmd, CancellationToken ct)
    {
        if (await _users.EmailExistsAsync(cmd.Email, ct))
            throw new InvalidOperationException("Email already registered.");

        var hash = _hasher.Hash(cmd.Password);
        var user = User.Create(cmd.Username, cmd.Email, hash);

        await _users.AddAsync(user, ct);
        await _uow.SaveChangesAsync(ct);

        return _tokens.GenerateToken(user);
    }
}
