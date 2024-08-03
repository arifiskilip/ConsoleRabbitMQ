using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: "amqps://uorpkohs:rxJ2I2ikiuaLDk4uj31Odn1_qLxPYf4L@jackal.rmq.cloudamqp.com/uorpkohs");
using var connection = factory.CreateConnection();
var channel = connection.CreateModel(); // Yeni bir kanal

var consumer = new EventingBasicConsumer(model:channel);
channel.BasicConsume(queue: "hello-queue", autoAck: true, consumer: consumer);

/*
	queue: Mesajların alınacağı kuyruk adıdır. Bu örnekte "hello-queue" olarak belirtilmiş. Bu, mesajların alınacağı kuyruktur.

	 autoAck: Otomatik onaylama (acknowledgement) davranışını belirler. true olarak ayarlandığında, RabbitMQ mesajı alındı olarak kabul eder ve mesajın kuyruğundan silinmesini otomatik olarak sağlar. false olarak ayarlandığında, mesajın başarıyla işlendiğini belirtmek için manuel olarak onaylanması gerekir. Manuel onaylama genellikle işleme sırasında bir hata meydana gelirse mesajın kuyruğa geri dönmesini sağlar.

	consumer: Mesajları işlemek için kullanılacak bir tüketici (consumer) nesnesidir. Bu nesne genellikle IConsumer arayüzünü implement eden bir sınıfın örneğidir ve mesaj alındığında hangi işlemlerin yapılacağını belirler. Tüketici, mesajları aldığında nasıl işleneceğini ve sonuç olarak ne yapılacağını tanımlar.
*/

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
	var message = Encoding.UTF8.GetString(e.Body.ToArray());
	Console.WriteLine("Mesajınız:" + message);
}