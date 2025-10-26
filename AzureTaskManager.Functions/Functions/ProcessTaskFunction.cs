using AzureTaskManager.Application.Interfaces;
using AzureTaskManager.Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureTaskManager.Functions.Functions
{
    public class ProcessTaskFunction
    {
        private readonly ITodoTaskService _taskService;
        private readonly ILogger _logger;

        public ProcessTaskFunction(ITodoTaskService taskService, ILoggerFactory loggerFactory)
        {
            _taskService = taskService;
            _logger = loggerFactory.CreateLogger<ProcessTaskFunction>();
        }

        [Function("ProcessTask")]
        public async Task Run(
            [QueueTrigger("nova-tarefa-queue", Connection = "AzureWebJobsStorage")] TodoTask task)
        {
            if (task is null || string.IsNullOrWhiteSpace(task.Title))
            {
                _logger.LogWarning("Tarefa inválida recebida da fila.");
                return;
            }

            try
            {
                await _taskService.CreateAsync(task);
                _logger.LogInformation($"Tarefa '{task.Title}' processada com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao processar tarefa '{task.Title}' (ID: {task.Id}).");
            }
        }
    }
}