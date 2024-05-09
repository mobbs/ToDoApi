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

    public Task<TodoItem> GetById(string id)
    {
        return _toDoItemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task Create(TodoItem toDo) => _toDoItemsCollection.InsertOneAsync(toDo);
    
    public Task Update(string id, TodoItem toDo) => _toDoItemsCollection.ReplaceOneAsync(x => x.Id == id, toDo);
    public Task Remove(string id) => _toDoItemsCollection.DeleteOneAsync(x => x.Id == id);
}