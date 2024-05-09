using Moq;
using ToDoApi.Controllers;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApiTests;

public class ToDoControllerTest
{
    private readonly Mock<IToDoService> _toDoService;
    private TodoItem _testTodo;

    public ToDoControllerTest()
    {
        _toDoService = new Mock<IToDoService>();
        _testTodo = new TodoItem { Id = "663b81458d9e481255821cf1", Description = "Do something" };
    }

    [Fact]
    public async void GetToDo_ShouldReturnList()
    {
        // Arrange
        _toDoService.Setup(x => x.GetAll())
            .Returns(Task.FromResult(new List<TodoItem> { _testTodo }));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.GetAllToDos();

        // Assert
        Assert.Single(result);
        Assert.Equal(_testTodo, result.First());
    }

    [Fact]
    public async void GetToDoById_ShouldReturnOne()
    {
        // Arrange
        var idToGet = _testTodo.Id!;
        _toDoService.Setup(x => x.GetById(idToGet))
            .Returns(Task.FromResult(_testTodo));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.GetToDoById(idToGet);

        // Assert
        Assert.Equal(idToGet, result.Id);
        Assert.Equal(_testTodo.Description, result.Description);
    }
}