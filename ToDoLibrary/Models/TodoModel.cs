namespace ToDoLibrary.Models;

public class TodoModel
{
    public int Id { get; set; }
    public string? TaskValue { get; set; }
    public int AssignedTo { get; set; }
    public bool IsComplete { get; set; }
}
