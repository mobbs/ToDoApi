using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoApi.Controllers;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApiTests;

public class ToDoControllerTest
{
    private readonly Mock<IToDoService> _toDoService = new();
    private readonly TodoItem _testTodo = new() { Id = "663b81458d9e481255821cf1", Description = "Do something" };

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
        Debug.Assert(result.Value != null, "result.Value != null");
        Assert.Equal(idToGet, result.Value.Id);
        Assert.Equal(_testTodo.Description, result.Value.Description);
    }
    
    [Fact]
    public async void GetToDoById_DoesNotExist()
    {
        // Arrange
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult<TodoItem>(null!));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.GetToDoById(_testTodo.Id!);

        //Assert
        Assert.NotNull(result);
        var actualResult = result.Result as NotFoundResult;
        Assert.NotNull(actualResult);
        Assert.Equal(404, actualResult.StatusCode);
    }
    
    [Fact]
    public async void PostTodo_ShouldCreateOne()
    {
        // Arrange
        _toDoService.Setup(x => x.Create(_testTodo))
            .Returns(Task.FromResult(_testTodo));
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult(_testTodo));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.PostToDo(_testTodo);

        // Assert
        Debug.Assert(result.Value != null, "result.Value != null");
        Assert.Equal(_testTodo.Id, result.Value.Id);
        Assert.Equal(_testTodo.Description, result.Value.Description);
    }
    
    [Fact]
    public async void PostTodo_DoesNotExist()
    {
        // Arrange
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult<TodoItem>(null!));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.PostToDo(_testTodo);

        //Assert
        Assert.NotNull(result);
        var actualResult = result.Result as NotFoundResult;
        Assert.NotNull(actualResult);
        Assert.Equal(404, actualResult.StatusCode);
    }
    
    [Fact]
    public async void PutTodo_ShouldCreateOne()
    {
        // Arrange
        _toDoService.Setup(x => x.Update(_testTodo.Id!, _testTodo))
            .Returns(Task.FromResult(_testTodo));
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult(_testTodo));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.PutToDo(_testTodo.Id!, _testTodo);

        // Assert
        Debug.Assert(result.Value != null, "result.Value != null");
        Assert.Equal(_testTodo.Id, result.Value.Id);
        Assert.Equal(_testTodo.Description, result.Value.Description);
    }
    
    [Fact]
    public async void PutTodo_DoesNotExist()
    {
        // Arrange
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult<TodoItem>(null!));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.PutToDo(_testTodo.Id!, _testTodo);

        //Assert
        Assert.NotNull(result);
        var actualResult = result.Result as NotFoundResult;
        Assert.NotNull(actualResult);
        Assert.Equal(404, actualResult.StatusCode);
    }
    
    [Fact]
    public async void DeleteTodo_ShouldRemoveOne()
    {
        // Arrange
        _toDoService.Setup(x => x.Remove(_testTodo.Id!))
            .Returns(Task.CompletedTask);
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult(_testTodo));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        var result = await toDoController.Delete(_testTodo.Id!) as NoContentResult;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }
    
    [Fact]
    public async void DeleteTodo_DoesNotExist()
    {
        // Arrange
        _toDoService.Setup(x => x.Remove(_testTodo.Id!))
            .Returns(Task.CompletedTask);
        _toDoService.Setup(x => x.GetById(_testTodo.Id!))
            .Returns(Task.FromResult<TodoItem>(null!));
        var toDoController = new ToDoController(_toDoService.Object);

        // Act
        Debug.Assert(_testTodo.Id != null, "_testTodo.Id != null");
        var result = await toDoController.Delete(_testTodo.Id) as NotFoundResult;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }
}