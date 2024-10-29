namespace Shared.DTOs;

public class SearchUserParameterDTO
{
    public string? UserNameContains {get;}
    
    public SearchUserParameterDTO(string? userNameContains)
    {
        UserNameContains = userNameContains;
    }
}