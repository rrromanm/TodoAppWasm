using System.Text.Json;
using Shared;

namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";

    private DataContainer? DataContainer;

    public ICollection<Todo> Todos
    {
        get
        {
            LazyLoadData();
            return DataContainer!.Todos;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LazyLoadData();
            return DataContainer!.Users;
        }
    }

    private void LazyLoadData()
    {
        if (DataContainer == null)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        string content = File.ReadAllText(FilePath);
        DataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(DataContainer);
        File.WriteAllText(FilePath, serialized);
        DataContainer = null;
    }
}