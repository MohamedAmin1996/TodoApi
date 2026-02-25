using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Domain.Entities;

namespace TodoApi.Infrastructure.Data.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Description).HasMaxLength(1000);
        builder.Property(t => t.Status).IsRequired();


        builder.HasOne(t => t.User)
               .WithMany(u => u.Todos)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(t => t.UserId);
        builder.ToTable("Todos");
    }
}
