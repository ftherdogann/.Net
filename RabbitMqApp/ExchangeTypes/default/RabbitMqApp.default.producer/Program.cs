using RabbitMQ.Client;
using System.Text;


var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();
channel.QueueDeclare("hello-queue", true, false, false);
string message = "hello.world first app";
var messageBody = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(String.Empty, "hello-queue", null, messageBody);
Console.WriteLine("mesaj gönderildi.");
Console.ReadLine();