using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Models;

public class TodoItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")] public string Description { get; set; } = null!;
}