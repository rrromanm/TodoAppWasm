using System.Text.Json;
using Shared;

namespace FileData;

public class FileContext
{
    private readonly string FilePath = "data.json";

    private DataContainer? dataContainer;

    public FileContext()
    {
        var initialData = new DataContainer
        {
            Todos = new List<Todo>(),
            Users = new List<User>()
        };
        File.WriteAllText(FilePath, JsonSerializer.Serialize(initialData));
    }
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
        var options = new JsonSerializerOptions
        {
            WriteIndented = true // Enables pretty-printing with indentation
        };
        string serialized = JsonSerializer.Serialize(dataContainer, options);
        File.WriteAllText(FilePath, serialized);
        dataContainer = null;
    }

}