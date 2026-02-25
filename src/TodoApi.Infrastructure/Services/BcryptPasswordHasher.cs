using TodoApi.Application.Interfaces;


namespace TodoApi.Infrastructure.Services;


// Liskov Substitution: BcryptPasswordHasher fulfils the IPasswordHasher
// contract completely. Any caller using IPasswordHasher will work correctly.
public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}
