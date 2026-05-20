namespace TaskManager.Application.DTOs;

public record TaskItemDto(Guid Id, string Title, bool IsCompleted, DateTime CreatedAt);
