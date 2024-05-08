using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{

    [HttpGet(Name = "GetToDo")]
    public IEnumerable<TodoItem> Get()
    {
        return new TodoItem[]
        {
            new TodoItem(1, "make dinner"),
            new TodoItem(2, "put out the washing")
        };
    }
}