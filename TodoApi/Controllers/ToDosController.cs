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

    public ToDosController(IToDoData data)
    {
        _data = data;
    }

    private int GetUserId()
    {
        return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
    }

    // GET: api/Todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoModel>>> Get()
    {
        var result = await _data.GetAllAssigned(GetUserId());

        return Ok(result);
    }

    // GET api/Todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        var result = await _data.GetOneAssigned(GetUserId(), todoId);

        return Ok(result);
    }

    // POST api/Todos
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string taskValue)
    {
        var result = _data.CreateTask(GetUserId(), taskValue);

        return Ok(result);
    }

    // PUT api/Todos/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}/Complete")]
    public IActionResult Complete(int id)
    {
        throw new NotImplementedException();
    }

    // DELETE api/Todos/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }
    
}
