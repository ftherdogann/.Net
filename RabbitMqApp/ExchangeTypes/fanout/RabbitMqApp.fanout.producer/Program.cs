using RabbitMQ.Client;
using System.Text;


var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
string exchangeName = "logs-fanout";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Fanout);

Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"logs-{x}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchangeName, "", null, messageBody);
    Console.WriteLine($"log gönderildi. {message}");

});

Console.ReadLine();