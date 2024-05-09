using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{

    private readonly IToDoService _toDoService;

    public ToDoController(IToDoService toDoService) => _toDoService = toDoService;

    
    [HttpGet(Name = "GetToDo")]
    public async Task<List<TodoItem>> GetAllToDos() => await _toDoService.GetAll();
    
    [HttpGet("{id:length(24)}")]
    public async Task<TodoItem> GetToDoById(string id) => await _toDoService.GetById(id);

    [HttpPost]
    public async Task<TodoItem> PostToDo(TodoItem toDo)
    {
        await _toDoService.CreateTodo(toDo);
        return await _toDoService.GetById(toDo.Id);
    }
    
}