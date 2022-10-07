using RabbitMQ.Client;

namespace ExcelCreate.Services
{
    public class RabbitMqClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExcangeName = "ExcelDirectExchange";
        public static string RoutingExcel = "excel-route-file";
        public static string QueueName = "queue-excel-file";

        private readonly ILogger<RabbitMqClientService> _logger;

        public RabbitMqClientService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExcangeName, "direct", true, false);

            _channel.QueueDeclare(QueueName, true, false, false);

            _channel.QueueBind(QueueName, ExcangeName, RoutingExcel);
            _logger.LogInformation("RabbitMq ile bağlantı kuruldu...");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _channel = default;
            _connection?.Close();
            _connection.Dispose();

            _logger.LogInformation("RabbitMq ile bağlantı koptu...");

        }
    }
}
