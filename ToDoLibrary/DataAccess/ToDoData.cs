using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess
{
    public class ToDoData : IToDoData
    {
        private readonly ISqlDataAccess _sql;

        public ToDoData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
        {
            return _sql.LoadData<TodoModel, dynamic>(
                "dbo.spTodos_GetAllAssigned",
                new
                {
                    AssignedTo = assignedTo
                },
                "Default");
        }

        public async Task<TodoModel?> GetOneAssigned(int assignedTo, int todoId)
        {
            var results = await _sql.LoadData<TodoModel, dynamic>(
                            "dbo.spTodos_GetOneAssigned",
                            new
                            {
                                AssignedTo = assignedTo,
                                id = todoId
                            },
                            "Default");

            return results.FirstOrDefault();
        }

        public async Task<TodoModel?> CreateTask(int assignedTo, string task)
        {
            var results = await _sql.LoadData<TodoModel, dynamic>(
                            "dbo.spTodos_Create",
                            new
                            {
                                assignedTo,
                                task
                            },
                            "Default");

            return results.FirstOrDefault();
        }

        public Task UpdateTask(int assignedTo, string task, int todoId)
        {
            return _sql.SaveData<dynamic>(
                "dbo.spTodos_Update",
                new
                {
                    task,
                    assignedTo,
                    todoId
                },
                "Default");
        }

        public Task CompleteTask(int assignedTo, int todoId)
        {
            return _sql.SaveData<dynamic>(
                "dbo.spTodos_Complete",
                new
                {
                    assignedTo,
                    todoId
                },
                "Default");
        }

        public Task DeleteTask(int assignedTo, string task, int todoId)
        {
            return _sql.SaveData<dynamic>(
                "dbo.spTodos_Delete",
                new
                {
                    assignedTo,
                    todoId
                },
                "Default");
        }
    }
}
