using Moq;
using ToDoApi.Controllers;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApiTests;

public class ToDoControllerTest
{
    private readonly Mock<IToDoService> _toDoService;

    public ToDoControllerTest()
    {
        _toDoService = new Mock<IToDoService>();
    }

    [Fact]
    public async void GetToDo_ShouldReturnList()
    {
        // Arrange
        var todo = new TodoItem { Id = "1", Description = "Do something" };
        _toDoService.Setup(x => x.GetAll())
            .Returns(Task.FromResult(new List<TodoItem> { todo }));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.Get();

        // Assert
        Assert.Single(result);
        Assert.Equal(todo, result.First());
    }
}