// Pasta: AzureTaskManager.Infrastructure/Services

using AzureTaskManager.Application.Interfaces;
using AzureTaskManager.Domain.Entities;
using AzureTaskManager.Domain.Enums;

namespace AzureTaskManager.Infrastructure.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _repository;

        public TodoTaskService(ITodoTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(TodoTask task)
        {
            await _repository.CreateAsync(task);
        }

        public async Task<List<TodoTask>> GetPendingAsync()
        {
            return await _repository.GetPendingAsync();
        }

        public async Task MarkAsCompletedAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task is not null)
            {
                task.Completed = true;
                task.Status = TaskState.Done;
                await _repository.UpdateAsync(task);
            }
        }

        public async Task UpdateStatusAsync(Guid id, TaskState newStatus)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task is not null)
            {
                task.Status = newStatus;
                await _repository.UpdateAsync(task);
            }
        }
    }
}