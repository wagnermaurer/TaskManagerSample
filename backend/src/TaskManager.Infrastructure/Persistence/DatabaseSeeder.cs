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

        for (var i = 1; i <= 10; i++)
        {
            db.Tasks.Add(new TaskItem($"TASK #{i} - Example"));
        }
        db.SaveChanges();
    }
}
