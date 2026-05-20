namespace TaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private TaskItem() { Title = string.Empty; }

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));

        Id = Guid.NewGuid();
        Title = title.Trim();
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void ToggleCompletion() => IsCompleted = !IsCompleted;
}
