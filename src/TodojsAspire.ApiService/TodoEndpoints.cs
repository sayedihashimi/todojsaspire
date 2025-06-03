using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodojsAspire.ApiService;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Todo");

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
            if (todo.Position <= 0) {
                // Get the current max position from the database
                int maxPosition = await db.Todo.MaxAsync(t => (int?)t.Position) ?? 0;
                todo.Position = maxPosition + 1;
            }
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

        // Endpoint to swap the position of two Todo items
        group.MapPost("/swap-position/{id1:int}/{id2:int}", async Task<Results<Ok, NotFound>> (int id1, int id2, TodoDbContext db) =>
        {
            var todo1 = await db.Todo.FirstOrDefaultAsync(t => t.Id == id1);
            var todo2 = await db.Todo.FirstOrDefaultAsync(t => t.Id == id2);

            if (todo1 == null || todo2 == null)
            {
                return TypedResults.NotFound();
            }

            // Swap the position values
            var temp = todo1.Position;
            todo1.Position = todo2.Position;
            todo2.Position = temp;

            await db.SaveChangesAsync();
            return TypedResults.Ok();
        })
        .WithName("SwapTodoPosition");
    }
}
