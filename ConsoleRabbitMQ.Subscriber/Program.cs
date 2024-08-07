using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: "amqps://uorpkohs:rxJ2I2ikiuaLDk4uj31Odn1_qLxPYf4L@jackal.rmq.cloudamqp.com/uorpkohs");
using var connection = factory.CreateConnection();
var channel = connection.CreateModel(); 

var randomQueueName = channel.QueueDeclare().QueueName; // Random queue name
channel.QueueBind(randomQueueName, "logs-fanout", string.Empty, null);


channel.BasicQos(0, 1, false);
var consumer = new EventingBasicConsumer(model:channel);

channel.BasicConsume(queue: randomQueueName, autoAck: false, consumer: consumer);

consumer.Received += Consumer_Received;

Console.WriteLine("Loglar dinleniyor....");
void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
	var message = Encoding.UTF8.GetString(e.Body.ToArray());
	Console.WriteLine("Log:" + message);

	channel.BasicAck(e.DeliveryTag, false);
}