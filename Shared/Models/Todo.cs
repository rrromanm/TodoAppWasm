namespace Shared;

public class Todo
{
    public int Id {get; set; }
    public User Owner {get; set; }
    public string Title {get; set; }
    public bool IsCompleted {get; set; } = false;

    public Todo(User user, string title)
    {
        Owner = user;
        Title = title;
    }
}