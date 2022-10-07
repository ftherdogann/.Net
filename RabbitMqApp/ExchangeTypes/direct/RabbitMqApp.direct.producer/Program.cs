using RabbitMQ.Client;
using RabbitMqApp.direct.producer;
using System.Text;


var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
string exchangeName = "logs-direct";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Direct);
var logNames = Enum.GetValues(typeof(LogTypes));
for (int i = 0; i < 11; i++)
{
    Random random = new Random();
    LogTypes logType = (LogTypes)logNames.GetValue(index: random.Next(logNames.Length));
    var body = Encoding.UTF8.GetBytes($"log={logType.ToString()}");
    channel.BasicPublish(exchangeName, routingKey: logType.ToString(), null, body: body);
}
Console.WriteLine("Mesaj Direct Exchange'e gönderilmiştir");
Console.ReadLine();