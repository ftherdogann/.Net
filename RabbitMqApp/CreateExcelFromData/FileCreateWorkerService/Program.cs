using FileCreateWorkerService;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        //IConfiguration configuration = hostContext.Configuration;
        services.AddSingleton<RabbitMqClientService>();
        services.AddDbContext<AdventureWorks2019Context>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        services.AddSingleton(sp => new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
