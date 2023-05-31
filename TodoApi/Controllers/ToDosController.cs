using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoLibrary.DataAccess;
using ToDoLibrary.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDosController : ControllerBase
{
    private readonly IToDoData _data;
    private ILogger _log;

    public ToDosController(IToDoData data, ILogger<ToDosController> log)
    {
        _data = data;
        _log = log;
    }

    private int GetUserId()
    {
        return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
    }

    // GET: api/Todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoModel>>> Get()
    {
        _log.LogInformation($"GET: api/Todos");

        try
        {
            var result = await _data.GetAllAssigned(GetUserId());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The Get call to api/Todos failed.");
            return BadRequest();
        }
    }

    // GET api/Todos/5
    [HttpGet("{todoId}")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        _log.LogInformation("GET: api/Todos/{TodoId}", todoId);

        try
        {
            var result = await _data.GetOneAssigned(GetUserId(), todoId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The GET call to {ApiPath} failed. The id was {todoId}", 
                $"api/Todos/{todoId}",
                todoId);
            return BadRequest();
        }
    }

    // POST api/Todos
    [HttpPost()]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string taskValue)
    {

        _log.LogInformation("POST: api/Todos/");

        try
        {
            var result = await _data.CreateTask(GetUserId(), taskValue);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The POST call to {ApiPath} failed. The id was {todoId}", $"api/Todos/");
            return BadRequest();
        }
    }

    // PUT api/Todos/5
    [HttpPut("{todoId}")]
    public async Task<ActionResult> Put(int todoId, [FromBody] string taskValue)
    {
        _log.LogInformation("PUT: api/Todos/{TodoId} (Task value: {taskValue})", todoId, taskValue);
        try
        {

            await _data.UpdateTask(GetUserId(), taskValue, todoId);
            return Ok();
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The PUT call to {ApiPath} failed. The id was {todoId}", $"api/Todos/");
            return BadRequest();
        }
    }

    // PUT api/Todos/5/Complete
    [HttpPut("{todoId}/Complete")]
    public async Task<ActionResult> Complete(int todoId)
    {
        _log.LogInformation("PUT: api/Todos/{TodoId}/Complete", todoId);
        
        try
        {
            await _data.CompleteTask(GetUserId(), todoId);
            return Ok();

        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The PUT call to {ApiPath} failed.", $"api/Todos/{todoId}/Complete");
            return BadRequest();
        }
    }

    // DELETE api/Todos/5
    [HttpDelete("{todoId}")]
    public async Task<ActionResult> Delete(int todoId)
    {
        _log.LogInformation("DELETE: api/Todos/{TodoId}", todoId);
        try
        {
            await _data.DeleteTask(GetUserId(), todoId);
            return Ok();
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "The DELETE call to {ApiPath} failed.", $"api/Todos/{todoId}");
            return BadRequest();
        }
    }
    
}
