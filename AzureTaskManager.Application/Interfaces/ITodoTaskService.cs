// Pasta: AzureTaskManager.Application/Interfaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTaskManager.Domain.Entities;
using AzureTaskManager.Domain.Enums;

namespace AzureTaskManager.Application.Interfaces
{
    public interface ITodoTaskService
    {
        Task CreateAsync(TodoTask task);
        Task<List<TodoTask>> GetPendingAsync();
        Task MarkAsCompletedAsync(Guid id);
        Task UpdateStatusAsync(Guid id, TaskState newStatus);
    }
}