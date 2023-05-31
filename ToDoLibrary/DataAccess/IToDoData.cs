using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess
{
    public interface IToDoData
    {
        Task CompleteTask(int assignedTo, int todoId);
        Task<TodoModel?> CreateTask(int assignedTo, string task);
        Task DeleteTask(int assignedTo, int todoId);
        Task<List<TodoModel>> GetAllAssigned(int assignedTo);
        Task<TodoModel?> GetOneAssigned(int assignedTo, int todoId);
        Task UpdateTask(int assignedTo, string task, int todoId);
    }
}