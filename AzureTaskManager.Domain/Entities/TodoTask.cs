// Pasta: AzureTaskManager.Domain/Entities

using System;
using AzureTaskManager.Domain.Enums;

namespace AzureTaskManager.Domain.Entities
{
    public class TodoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TaskState Status { get; set; } = TaskState.ToDo;
         public TodoTask(string title, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        DueDate = dueDate;
        Completed = false;
        CreatedAt = DateTime.UtcNow;
        Status = 0;
    }

    // Para EF Core
    private TodoTask() { }


    }
}