using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;

namespace TodoApi.Infrastructure.Data;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<TodoItem> Todos => Set<TodoItem>();
    public DbSet<User> Users => Set<User>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }


    // IUnitOfWork implementation
    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        => base.SaveChangesAsync(ct);
}
