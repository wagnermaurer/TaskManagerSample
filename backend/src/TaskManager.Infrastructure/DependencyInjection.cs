using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TaskDbContext>(options =>
            options.UseInMemoryDatabase("TaskManagerDb"));

        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }
}
