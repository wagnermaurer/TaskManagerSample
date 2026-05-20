using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using Xunit;

namespace TaskManager.Tests.Application;

public class TaskServiceTests
{
    [Fact]
    public async Task CreateAsync_PersistsAndReturnsDto()
    {
        var repo = new FakeTaskRepository();
        var service = new TaskService(repo);

        var dto = await service.CreateAsync(new CreateTaskRequest { Title = "Ship it" });

        Assert.Equal("Ship it", dto.Title);
        Assert.False(dto.IsCompleted);
        Assert.Single(repo.Store);
    }

    [Fact]
    public async Task ToggleAsync_ReturnsNull_WhenTaskMissing()
    {
        var service = new TaskService(new FakeTaskRepository());

        var result = await service.ToggleAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task ToggleAsync_FlipsCompletion()
    {
        var repo = new FakeTaskRepository();
        var existing = new TaskItem("Read book");
        await repo.AddAsync(existing);
        var service = new TaskService(repo);

        var first = await service.ToggleAsync(existing.Id);
        var second = await service.ToggleAsync(existing.Id);

        Assert.True(first!.IsCompleted);
        Assert.False(second!.IsCompleted);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_AndRemovesTask_WhenFound()
    {
        var repo = new FakeTaskRepository();
        var existing = new TaskItem("Throw away");
        await repo.AddAsync(existing);
        var service = new TaskService(repo);

        var deleted = await service.DeleteAsync(existing.Id);

        Assert.True(deleted);
        Assert.Empty(repo.Store);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenTaskMissing()
    {
        var service = new TaskService(new FakeTaskRepository());

        var deleted = await service.DeleteAsync(Guid.NewGuid());

        Assert.False(deleted);
    }

    private sealed class FakeTaskRepository : ITaskRepository
    {
        public Dictionary<Guid, TaskItem> Store { get; } = new();

        public Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<TaskItem>>(Store.Values.ToList());

        public Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            Task.FromResult(Store.TryGetValue(id, out var t) ? t : null);

        public Task AddAsync(TaskItem task, CancellationToken ct = default)
        {
            Store[task.Id] = task;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskItem task, CancellationToken ct = default)
        {
            Store[task.Id] = task;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskItem task, CancellationToken ct = default)
        {
            Store.Remove(task.Id);
            return Task.CompletedTask;
        }
    }
}
