using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTOs;

public class CreateTaskRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;
}
