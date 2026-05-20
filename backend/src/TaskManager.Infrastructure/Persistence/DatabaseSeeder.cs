using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    // Hardcoded on purpose: the in-memory DB resets on every API restart.
    // Ten example tasks is enough to demo the UI without adding a config layer.
    public static void SeedTasks(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

        if (db.Tasks.Any()) return;

        var now = DateTime.UtcNow;
        
        for (var i = 1; i <= 10; i++)
        {
            var task = new TaskItem($"TASK #{i} - Example");
            db.Tasks.Add(task);
            db.Entry(task).Property(t => t.CreatedAt).CurrentValue = now.AddHours(-(i - 1));
        }
        db.SaveChanges();
    }
}
