using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController(IToDoService toDoService) : ControllerBase
{
    [HttpGet(Name = "GetToDo")]
    public async Task<List<TodoItem>> GetAllToDos() => await toDoService.GetAll();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TodoItem>> GetToDoById(string id)
    {
        var toDo = await toDoService.GetById(id);
        return (toDo is null) ? NotFound() : toDo;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostToDo(TodoItem toDo)
    {
        await toDoService.Create(toDo);
        var createdToDo = await toDoService.GetById(toDo.Id);
        return (createdToDo is null) ? NotFound() : createdToDo;
        // CreatedAtAction(nameof(GetAllToDos), new { id = toDo.Id }, toDo);
    }
    
    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult<TodoItem>> PutToDo(string id, TodoItem toDo)
    {
        var existingToDo = await toDoService.GetById(id);

        if (existingToDo is null)
        {
            return NotFound();
        }

        toDo.Id = existingToDo.Id;
        await toDoService.Update(id, toDo);
        return await toDoService.GetById(id);
    }
    
    [HttpDelete("{id:length(24)}")]
    public async Task<ActionResult> Delete(string id)
    {
        var existingToDo = await toDoService.GetById(id);
        
        if (existingToDo is null)
        {
            return NotFound();
        }

        await toDoService.Remove(id);
        return NoContent();
    }
    
}