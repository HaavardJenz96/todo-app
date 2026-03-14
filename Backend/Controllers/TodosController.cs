using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(TodoService todoService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Todo>> GetAll()
    {
        return Ok(todoService.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = todoService.GetById(id);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<Todo> Create(CreateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            ModelState.AddModelError(nameof(request.Title), "Title is required.");
            return ValidationProblem(ModelState);
        }

        var todo = todoService.Create(request.Title);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Todo> Update(int id, UpdateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            ModelState.AddModelError(nameof(request.Title), "Title is required.");
            return ValidationProblem(ModelState);
        }

        var todo = todoService.Update(id, request.Title, request.IsCompleted);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return todoService.Delete(id) ? NoContent() : NotFound();
    }
}

public record CreateTodoRequest(string Title);

public record UpdateTodoRequest(string Title, bool IsCompleted);
