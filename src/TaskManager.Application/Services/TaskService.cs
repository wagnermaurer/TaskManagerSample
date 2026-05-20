using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TaskItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _repository.GetAllAsync(cancellationToken);
        return tasks.Select(ToDto).ToList();
    }

    public async Task<TaskItemDto> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        var task = new TaskItem(request.Title);
        await _repository.AddAsync(task, cancellationToken);
        return ToDto(task);
    }

    public async Task<TaskItemDto?> ToggleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken);
        if (task is null) return null;

        task.ToggleCompletion();
        await _repository.UpdateAsync(task, cancellationToken);
        return ToDto(task);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken);
        if (task is null) return false;

        await _repository.DeleteAsync(task, cancellationToken);
        return true;
    }

    private static TaskItemDto ToDto(TaskItem task) =>
        new(task.Id, task.Title, task.IsCompleted, task.CreatedAt);
}
