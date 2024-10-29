using System.Text.Json;
using Shared;

namespace FileData;

public class FileContext
{
    private readonly string FilePath = "data.json";

    private DataContainer? dataContainer;
    public ICollection<Todo> Todos
    {
        get
        {
            LoadData();
            return dataContainer!.Todos;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }


    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(FilePath))
        {
            dataContainer = new ()
            {
                Todos = new List<Todo>(),
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(FilePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, serialized);
        dataContainer = null;
    }

}