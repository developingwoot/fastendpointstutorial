namespace VerticalSliceFastEndpoints.Entities;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;

    public TodoItem() { }

    public TodoItem(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
    }
}