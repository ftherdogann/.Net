using RabbitMQ.Client;
using RabbitMqApp.complextype.producer;
using RabbitMqApp.complextype.shared;
using System.Text;
using System.Text.Json;

var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
string exchangeName = "headers-exchange";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Headers);

Dictionary<string, object> headers = new Dictionary<string, object>();

headers.Add("format", "pdf");
headers.Add("shape", "a4");
var properties = channel.CreateBasicProperties();
properties.Headers = headers;
properties.Persistent = true; // mesajların kalıcı olması için 

var exampleProduct = new Product { Id = 1, Name = "Kalem", Price = 100, Stock = 99 };
var productJsonString = JsonSerializer.Serialize(exampleProduct);

channel.BasicPublish(exchangeName, string.Empty, properties, Encoding.UTF8.GetBytes(productJsonString));
Console.WriteLine("mesaj gönderilmiştir.");
Console.ReadLine();