using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodojsAspire.ApiService;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Todo");

        group.MapGet("/", async (TodoDbContext db) =>
        {
            return await db.Todo.ToListAsync();
        })
        .WithName("GetAllTodos");

        group.MapGet("/{id}", async Task<Results<Ok<Todo>, NotFound>> (int id, TodoDbContext db) =>
        {
            return await db.Todo.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Todo model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTodoById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Todo todo, TodoDbContext db) =>
        {
            var affected = await db.Todo
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.Title, todo.Title)
                .SetProperty(m => m.IsComplete, todo.IsComplete)
                .SetProperty(m => m.Position, todo.Position)
        );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTodo");

        group.MapPost("/", async (Todo todo, TodoDbContext db) =>
        {
            db.Todo.Add(todo);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Todo/{todo.Id}",todo);
        })
        .WithName("CreateTodo");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, TodoDbContext db) =>
        {
            var affected = await db.Todo
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTodo");
    }
}
