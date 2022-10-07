using RabbitMQ.Client;
using Shared;
using System.Text;
using System.Text.Json;

namespace ExcelCreate.Services
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqClientService _rabbitMqClientService;

        public RabbitMqPublisher(RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqClientService = rabbitMqClientService;
        }

        public void Publish(CreateExcelMessage createExcelMessage)
        {
            var channel = _rabbitMqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(createExcelMessage);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMqClientService.ExcangeName, routingKey: RabbitMqClientService.RoutingExcel, basicProperties: properties, body: bodyByte);
        }
    }
}
