using TodoApi.Domain.Enums;


namespace TodoApi.Domain.Entities;


public class TodoItem : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public TodoStatus Status { get; private set; } = TodoStatus.Pending;
    public DateTime? DueDate { get; private set; }
    public Guid UserId { get; private set; }
    public User? User { get; private set; }


    public static TodoItem Create(string title, string? description, DateTime? dueDate, Guid userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.");

        return new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            DueDate = dueDate,
            UserId = userId,
            Status = TodoStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }


    public void Update(string title, string? description, DateTime? dueDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        Title = title;
        Description = description;
        DueDate = dueDate;
        SetUpdated();
    }


    public void Complete()
    {
        if (Status == TodoStatus.Completed)
            throw new InvalidOperationException("Todo is already completed.");
        Status = TodoStatus.Completed;
        SetUpdated();
    }


    public void Delete() { Status = TodoStatus.Deleted; SetUpdated(); }
}
