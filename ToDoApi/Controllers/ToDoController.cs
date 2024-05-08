using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{

    private readonly ToDoService _toDoService;

    public ToDoController(ToDoService toDoService) => _toDoService = toDoService;

    
    [HttpGet(Name = "GetToDo")]
    public async Task<List<TodoItem>> Get() => await _toDoService.GetAsync();
    /*
    {
        
        return new TodoItem[]
        {
            new TodoItem(1, "make dinner"),
            new TodoItem(2, "put out the washing")
        };
    }
    */
}