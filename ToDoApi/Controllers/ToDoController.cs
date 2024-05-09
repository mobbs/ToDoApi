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
    public async Task<ActionResult<TodoItem>> GetToDoById(string id)
    {
        var toDo = await _toDoService.GetById(id);
        return (toDo is null) ? NotFound() : toDo;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostToDo(TodoItem toDo)
    {
        await _toDoService.Create(toDo);
        var createdToDo = await _toDoService.GetById(toDo.Id);
        return (createdToDo is null) ? NotFound() : createdToDo;
    }
    
    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult<TodoItem>> PutToDo(string id, TodoItem toDo)
    {
        var existingToDo = await _toDoService.GetById(id);

        if (existingToDo is null)
        {
            return NotFound();
        }

        toDo.Id = existingToDo.Id;
        await _toDoService.Update(id, existingToDo);
        return await _toDoService.GetById(id);
    }
    
}