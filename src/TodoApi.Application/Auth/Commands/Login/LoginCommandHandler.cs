using MediatR;
using TodoApi.Application.Common.Exceptions;
using TodoApi.Application.Interfaces;


namespace TodoApi.Application.Auth.Commands.Login;


public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenService _tokens;


    public LoginCommandHandler(
        IUserRepository users,
        IPasswordHasher hasher,
        ITokenService tokens)
    {
        _users = users; _hasher = hasher; _tokens = tokens;
    }


    public async Task<string> Handle(LoginCommand cmd, CancellationToken ct)
    {
        var user = await _users.GetByEmailAsync(cmd.Email, ct)
            ?? throw new UnauthorizedException("Invalid email or password.");


        if (!_hasher.Verify(cmd.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");


        return _tokens.GenerateToken(user);
    }
}
