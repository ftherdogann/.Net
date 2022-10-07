using RabbitMQ.Client;
using RabbitMqApp.topic.producer;
using System.Text;


var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
string exchangeName = "logs-topic";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Topic);

Random random = new Random();
Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    LogTypes log = (LogTypes)new Random().Next(1, 5);

   
    LogTypes log1 = (LogTypes)random.Next(1, 5);
    LogTypes log2 = (LogTypes)random.Next(1, 5);
    LogTypes log3 = (LogTypes)random.Next(1, 5);

    string routeKey = $"{log1}.{log2}.{log3}";
    string message = $"log-type: {log1}-{log2}-{log3}";
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchangeName, routeKey, null, body);

    Console.WriteLine($"Mesaj Exchange'e gönderilmiştir.{message}");
});

Console.ReadLine();