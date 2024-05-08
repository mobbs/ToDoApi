namespace ToDoApi.Models;

public class ToDoItemsDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ToDoItemsCollectionName { get; set; } = null!;
}