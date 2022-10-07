using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
string exchangeName = "logs-topic";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Topic);

var queueName = channel.QueueDeclare().QueueName;
var routeKey = "*.Warning.#";
channel.QueueBind(queue: queueName, exchangeName, routingKey: routeKey);

channel.BasicQos(0, 1, false);
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queueName, false, consumer: consumer);
Console.WriteLine("loglar dinleniyor...");
consumer.Received += (render, argument) =>
{
    string message = Encoding.UTF8.GetString(argument.Body.ToArray());
    Console.WriteLine(message);
    channel.BasicAck(deliveryTag: argument.DeliveryTag, false);

};
Console.ReadLine();