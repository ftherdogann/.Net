using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//bir exchange belirtilmediğinde defalut exchange üzerinden direk kuyruğa mesaj gönderilir.
var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
using var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("hello-queue", true, consumer);// kuyruktan mesaj okunur. autoark false olursa işlenen mesajı kuyruktan silmek için bildirim bekler.true ise direk siler.
Console.WriteLine("loglar dinleniyor....");
consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var receiveMessage = Encoding.UTF8.GetString(e.Body.ToArray());   
    Console.WriteLine("Gelen Message:" + receiveMessage);

}

Console.ReadLine();