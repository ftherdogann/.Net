using RabbitMQ.Client;
using RabbitMqApp.headers.producer;
using System.Text;


var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
string exchangeName = "headers-exchange";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Headers);

Dictionary<string,object> headers = new Dictionary<string, object>();

headers.Add("format", "pdf");
headers.Add("shape", "a4");
var properties = channel.CreateBasicProperties();
properties.Headers = headers;
properties.Persistent = true; // mesajların kalıcı olması için 
channel.BasicPublish(exchangeName, string.Empty, properties,Encoding.UTF8.GetBytes("header mesajım"));
Console.WriteLine("mesaj gönderilmiştir.");
Console.ReadLine();