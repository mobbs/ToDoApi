using ToDoApi.Models;

namespace ToDoApi.Services;

public interface IToDoService
{
    Task<List<TodoItem>> GetAll();
    Task<TodoItem> GetById(string id);
    Task Create(TodoItem toDo);
    Task Update(string id, TodoItem toDo);
    Task Remove(string id);
}