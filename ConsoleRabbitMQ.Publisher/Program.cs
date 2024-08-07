using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: "amqps://uorpkohs:rxJ2I2ikiuaLDk4uj31Odn1_qLxPYf4L@jackal.rmq.cloudamqp.com/uorpkohs");
using var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs-fanout", durable: true, type: ExchangeType.Fanout);

Enumerable.Range(1, 20).ToList().ForEach(x =>
{
	string message = $"Log {x}";
	var messageBody = Encoding.UTF8.GetBytes(message);
	channel.BasicPublish(exchange: "logs-fanout",
		routingKey: string.Empty, basicProperties: null, body: messageBody);

	Console.WriteLine($"Mesajınız gönderildi {message}");
});

