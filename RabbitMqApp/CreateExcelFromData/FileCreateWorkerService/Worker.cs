using ClosedXML.Excel;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System.Data;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace FileCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqClientService _rabbitMqClientService;
        private IModel _channel;
        private readonly IServiceProvider _serviceProdiver;
        public Worker(ILogger<Worker> logger, RabbitMqClientService rabbitMqClientService, IServiceProvider serviceProdiver)
        {
            _logger = logger;
            _rabbitMqClientService = rabbitMqClientService;
            _serviceProdiver = serviceProdiver;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMqClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);

            var excel = JsonSerializer.Deserialize<CreateExcelMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            using var ms = new MemoryStream();

            var workbook = new XLWorkbook();
            var dataset = new DataSet();

            dataset.Tables.Add(GetTable("products"));
            workbook.Worksheets.Add(dataset);

            workbook.SaveAs(ms);

            MultipartFormDataContent multipartFormData = new MultipartFormDataContent();
            multipartFormData.Add(new ByteArrayContent(ms.ToArray()),"file",Guid.NewGuid().ToString()+".xlsx");

            var baseUrl = "https://localhost:44365/api/files";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync($"{baseUrl}?fileId={excel.FileId}", multipartFormData);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"File ( Id : {excel.FileId} was created by successfully....");
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
        }

        private DataTable GetTable(string tableName)
        {
            List<Product> products;

            using (var scope = _serviceProdiver.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AdventureWorks2019Context>();
                products = context.Products.ToList();
            }
            DataTable table = new DataTable() { TableName = tableName };

            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("ProductNumber", typeof(string));
            table.Columns.Add("Color", typeof(string));
            products.ForEach(x =>
            {
                table.Rows.Add(x.ProductId, x.Name, x.ProductNumber, x.Color);
            });
            return table;
        }
    }
}