using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Persistence;
using Xunit;

namespace TaskManager.Tests.Infrastructure;

public class DatabaseSeederTests
{
    // Each call gets its own DB so tests are isolated. The name is captured
    // outside the lambda so every scope in this provider shares the same store.
    private static IServiceProvider BuildProvider()
    {
        var dbName = Guid.NewGuid().ToString();
        return new ServiceCollection()
            .AddDbContext<TaskDbContext>(o => o.UseInMemoryDatabase(dbName))
            .BuildServiceProvider();
    }

    private static TaskDbContext OpenDb(IServiceProvider provider) =>
        provider.CreateScope().ServiceProvider.GetRequiredService<TaskDbContext>();

    [Fact]
    public void SeedTasks_AddsTenTasksWithExpectedTitles()
    {
        var provider = BuildProvider();

        provider.SeedTasks();

        var titles = OpenDb(provider).Tasks.Select(t => t.Title).ToList();
        Assert.Equal(10, titles.Count);
        for (var i = 1; i <= 10; i++)
            Assert.Contains($"TASK #{i} - Example", titles);
    }

    [Fact]
    public void SeedTasks_IsIdempotent()
    {
        var provider = BuildProvider();

        provider.SeedTasks();
        provider.SeedTasks();

        Assert.Equal(10, OpenDb(provider).Tasks.Count());
    }

    [Fact]
    public void SeedTasks_SpreadsTimestampsOneHourApart()
    {
        var provider = BuildProvider();

        provider.SeedTasks();

        var ordered = OpenDb(provider).Tasks
            .OrderByDescending(t => t.CreatedAt)
            .ToList();

        Assert.Equal("TASK #1 - Example", ordered.First().Title);
        Assert.Equal("TASK #10 - Example", ordered.Last().Title);

        for (var i = 1; i < ordered.Count; i++)
            Assert.Equal(TimeSpan.FromHours(1), ordered[i - 1].CreatedAt - ordered[i].CreatedAt);
    }
}
