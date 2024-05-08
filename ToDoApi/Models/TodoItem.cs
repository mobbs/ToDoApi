namespace ToDoApi.Models;

public class TodoItem
{
    public int id  { get; set; }
    public string name  { get; set; }

    public TodoItem(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}