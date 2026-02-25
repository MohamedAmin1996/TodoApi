using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;
using TodoApi.Infrastructure.Data;


namespace TodoApi.Infrastructure.Repositories;


public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;


    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _ctx.Users
            .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), ct);


    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _ctx.Users.FindAsync(new object[] { id }, ct);


    public async Task AddAsync(User user, CancellationToken ct = default)
        => await _ctx.Users.AddAsync(user, ct);


    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        => await _ctx.Users.AnyAsync(u => u.Email == email.ToLowerInvariant(), ct);
}
