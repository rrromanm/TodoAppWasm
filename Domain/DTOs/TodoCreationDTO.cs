namespace Shared.DTOs;

public class TodoCreationDTO
{
    public int UserId { get; }
    public string Title { get; }
    
    public TodoCreationDTO(int userId, string title)
    {
        UserId = userId;
        Title = title;
    }
    
}