// Pasta: AzureTaskManager.Application/Interfaces

using AzureTaskManager.Domain.Entities;

namespace AzureTaskManager.Application.Interfaces
{
    public interface ITodoTaskRepository
    {
        Task CreateAsync(TodoTask task);
        Task<List<TodoTask>> GetPendingAsync();
        Task<TodoTask?> GetByIdAsync(Guid id);
        Task UpdateAsync(TodoTask task);
    }
}