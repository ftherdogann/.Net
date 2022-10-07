using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";

using var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();
string exchangeName = "logs-fanout";
channel.BasicQos(0, 1, false); // son parameter false olursa her subcriber a 1 er 1 er gönderir true olursa prefectCount değerini kaç tane instance varsa ona böler ve paylaştırır.
var consumer = new EventingBasicConsumer(channel);
var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queueName, exchangeName, "", null);

channel.BasicConsume(queueName, false, consumer);// kuyruktan mesaj okunur. autoark false olursa işlenen mesajı kuyruktan silmek için bildirim bekler.true ise direk siler.
Console.WriteLine("loglar dinleniyor....");
consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
        var receiveMessage = Encoding.UTF8.GetString(e.Body.ToArray());
        Thread.Sleep(1500);
        Console.WriteLine("Gelen Message:" + receiveMessage);
        channel.BasicAck(e.DeliveryTag, false); // mesajı işlediğimize ve silinebileceğine dair geri rabbitmq ya bilidirm yapıyoruz.
}

Console.ReadLine();