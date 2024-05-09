using ToDoApi.Models;

namespace ToDoApi.Services;

public interface IToDoService
{
    Task<List<TodoItem>> GetAll();
    Task<TodoItem> GetById(string id);
    Task CreateTodo(TodoItem toDo);
}