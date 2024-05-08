using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoApi.Models;

namespace ToDoApi.Services;

public class ToDoService : IToDoService
{
    private readonly IMongoCollection<TodoItem> _toDoItemsCollection;

    public ToDoService(IOptions<ToDoItemsDatabaseSettings> toDoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            toDoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            toDoDatabaseSettings.Value.DatabaseName);

        _toDoItemsCollection = mongoDatabase.GetCollection<TodoItem>(
            toDoDatabaseSettings.Value.ToDoItemsCollectionName);
    }

    public async Task<List<TodoItem>> GetAll() =>
        await _toDoItemsCollection.Find(_ => true).ToListAsync();
}