namespace Shared.DTOs;

public class UserCreationDTO
{
    public string username { get; set; }

    public UserCreationDTO(string username)
    {
        this.username = username;
    }
}