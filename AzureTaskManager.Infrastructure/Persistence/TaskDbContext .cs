// Pasta: AzureTaskManager.Infrastructure/Persistence

using Microsoft.EntityFrameworkCore;
using AzureTaskManager.Domain.Entities;

namespace AzureTaskManager.Infrastructure.Persistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TodoTask> TodoTask => Set<TodoTask>();
    }
}