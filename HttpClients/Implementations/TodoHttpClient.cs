using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared;
using Shared.DTOs;

namespace HttpClients.Implementations;

public class TodoHttpClient : ITodoService
{
    private readonly HttpClient client;

    public TodoHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(TodoCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7090/Todo", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}