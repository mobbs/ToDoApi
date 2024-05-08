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
    public async Task<List<TodoItem>> Get() => await _toDoService.GetAll();
}