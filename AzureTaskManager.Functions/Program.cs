using Azure.Storage.Queues;
using AzureTaskManager.Application.Interfaces;
using AzureTaskManager.Infrastructure.Persistence;
using AzureTaskManager.Infrastructure.Repositories;
using AzureTaskManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        var queueConnection = context.Configuration["AzureWebJobsStorage"];
        var queueClient = new QueueClient(queueConnection, "nova-tarefa-queue");
        queueClient.CreateIfNotExists();
        services.AddSingleton(queueClient);

        var dbConnection = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TaskDbContext>(options =>
        {
            options.UseSqlServer(dbConnection);
        });

        // ✅ Registro completo
        services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
        services.AddScoped<ITodoTaskService, TodoTaskService>();
    })
    .Build();

await host.RunAsync();