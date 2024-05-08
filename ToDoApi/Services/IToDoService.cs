using ToDoApi.Models;

namespace ToDoApi.Services;

public interface IToDoService
{
    Task<List<TodoItem>> GetAll();
}