using TaskManager.Domain.Entities;
using Xunit;

namespace TaskManager.Tests.Domain;

public class TaskItemTests
{
    [Fact]
    public void Constructor_SetsDefaults_AndTrimsTitle()
    {
        var task = new TaskItem("  Write tests  ");

        Assert.NotEqual(Guid.Empty, task.Id);
        Assert.Equal("Write tests", task.Title);
        Assert.False(task.IsCompleted);
        Assert.True(task.CreatedAt <= DateTime.UtcNow);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ThrowsOnEmptyTitle(string? title)
    {
        Assert.Throws<ArgumentException>(() => new TaskItem(title!));
    }

    [Fact]
    public void ToggleCompletion_FlipsState()
    {
        var task = new TaskItem("Buy milk");

        task.ToggleCompletion();
        Assert.True(task.IsCompleted);

        task.ToggleCompletion();
        Assert.False(task.IsCompleted);
    }
}
