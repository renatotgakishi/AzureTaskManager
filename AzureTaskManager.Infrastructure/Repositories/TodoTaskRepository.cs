// Pasta: AzureTaskManager.Infrastructure/Repositories

using AzureTaskManager.Application.Interfaces;
using AzureTaskManager.Domain.Entities;
using AzureTaskManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzureTaskManager.Infrastructure.Repositories
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly TaskDbContext _context;

        public TodoTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TodoTask task)
        {
            _context.TodoTask.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TodoTask>> GetPendingAsync()
        {
            return await _context.TodoTask
                .Where(t => !t.Completed && t.DueDate >= DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<TodoTask?> GetByIdAsync(Guid id)
        {
            return await _context.TodoTask.FindAsync(id);
        }

        public async Task UpdateAsync(TodoTask task)
        {
            _context.TodoTask.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}