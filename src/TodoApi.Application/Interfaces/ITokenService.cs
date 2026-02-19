using TodoApi.Domain.Entities;

namespace TodoApi.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
