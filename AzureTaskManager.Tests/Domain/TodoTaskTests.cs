using AzureTaskManager.Domain.Entities;
using FluentAssertions;

namespace AzureTaskManager.Tests.Domain;

public class TodoTaskTests
{
    [Fact]
    public void Should_Initialize_Task_With_Defaults()
    {
        var task = new TodoTask("Estudar testes", DateTime.UtcNow.AddDays(1));

        task.Title.Should().Be("Estudar testes");
        task.Completed.Should().BeFalse();
        task.Status.Should().Be(0);
        task.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}