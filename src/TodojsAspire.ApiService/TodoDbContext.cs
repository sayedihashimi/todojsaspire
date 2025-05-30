using Microsoft.EntityFrameworkCore;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<TodojsAspire.ApiService.Todo> Todo { get; set; } = default!;
}
