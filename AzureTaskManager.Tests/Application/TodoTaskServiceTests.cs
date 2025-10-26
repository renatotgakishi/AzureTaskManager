using AzureTaskManager.Application.Interfaces;
using AzureTaskManager.Domain.Entities;
using AzureTaskManager.Infrastructure.Services;
using Moq;

namespace AzureTaskManager.Tests.Application;

public class TodoTaskServiceTests
{
    private readonly Mock<ITodoTaskRepository> _repoMock = new();
    private readonly TodoTaskService _service;

    public TodoTaskServiceTests()
    {
        _service = new TodoTaskService(_repoMock.Object);
    }

    [Fact]
    public async Task Should_Call_Repository_When_Creating_Task()
    {
        var task = new TodoTask("Nova tarefa", DateTime.UtcNow.AddDays(1));

        await _service.CreateAsync(task);

        _repoMock.Verify(r => r.CreateAsync(task), Times.Once);
    }
}