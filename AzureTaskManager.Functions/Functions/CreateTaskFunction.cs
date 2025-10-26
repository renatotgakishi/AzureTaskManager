using System.Net;
using System.Text.Json;
using Azure.Storage.Queues;
using AzureTaskManager.Domain.Entities;
using AzureTaskManager.Domain.Enums;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureTaskManager.Functions.Functions
{
    public class CreateTaskFunction
    {
        private readonly ILogger _logger;
        private readonly QueueClient _queueClient;

        public CreateTaskFunction(ILoggerFactory loggerFactory, QueueClient queueClient)
        {
            _logger = loggerFactory.CreateLogger<CreateTaskFunction>();
            _queueClient = queueClient;
        }
        // Construtor simplificado (usado em testes)
        public  CreateTaskFunction(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }




        [Function("CreateTask")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "tasks")] HttpRequestData req)
        {
            try
            {
                var body = await JsonSerializer.DeserializeAsync<TodoTask>(req.Body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (body is null || string.IsNullOrWhiteSpace(body.Title))
                {
                    var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                    await badRequest.WriteStringAsync("Título da tarefa é obrigatório.");
                    return badRequest;
                }

                body.Status = TaskState.ToDo;
                body.CreatedAt = DateTime.UtcNow;
                body.Completed = false;

                var json = JsonSerializer.Serialize(body);
                await _queueClient.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json)));

                var response = req.CreateResponse(HttpStatusCode.Accepted);
                await response.WriteStringAsync("Tarefa enviada para processamento.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar tarefa para fila.");
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteStringAsync("Erro interno ao enviar tarefa.");
                return error;
            }
        }
    }
}