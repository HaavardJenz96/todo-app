using Backend.Models;

namespace Backend.Services;

public class TodoService
{
    private readonly List<Todo> _todos =
    [
        new() { Id = 1, Title = "Design login page", IsCompleted = true, CompletedAt = DateTime.UtcNow.AddDays(-2) },
        new() { Id = 2, Title = "Setup backend API", IsCompleted = true, CompletedAt = DateTime.UtcNow.AddDays(-1) },
        new() { Id = 3, Title = "Connect frontend to API" }
    ];

    private int _nextId = 4;

    public IReadOnlyList<Todo> GetAll()
    {
        return _todos
            .OrderBy(todo => todo.IsCompleted)
            .ThenByDescending(todo => todo.CreatedAt)
            .ToList();
    }

    public Todo? GetById(int id)
    {
        return _todos.FirstOrDefault(todo => todo.Id == id);
    }

    public Todo Create(string title)
    {
        var todo = new Todo
        {
            Id = _nextId++,
            Title = title.Trim()
        };

        _todos.Add(todo);
        return todo;
    }

    public Todo? Update(int id, string title, bool isCompleted)
    {
        var todo = GetById(id);
        if (todo is null)
        {
            return null;
        }

        todo.Title = title.Trim();
        todo.IsCompleted = isCompleted;
        todo.CompletedAt = isCompleted ? DateTime.UtcNow : null;

        return todo;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo is null)
        {
            return false;
        }

        _todos.Remove(todo);
        return true;
    }
}
