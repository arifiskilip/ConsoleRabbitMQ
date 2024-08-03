using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: "amqps://uorpkohs:rxJ2I2ikiuaLDk4uj31Odn1_qLxPYf4L@jackal.rmq.cloudamqp.com/uorpkohs");
using var connection = factory.CreateConnection();
var channel = connection.CreateModel(); // Yeni bir kanal

channel.QueueDeclare(queue:"hello-queue",durable:true,exclusive:false, autoDelete:false);

/* queue: Oluşturulacak veya kontrol edilecek kuyruk adıdır. Örneğin, "hello-queue".

durable: Kuyruğun kalıcılığını belirler. true olarak ayarlanırsa, kuyruk RabbitMQ sunucusu yeniden başlatılsa bile kuyruk kaybolmaz. Bu, kuyrukta kalıcı verilerin olması gerektiğinde kullanılır. false olarak ayarlanırsa, kuyruk sadece sunucu çalıştığı sürece var olur ve sunucu yeniden başlatıldığında kaybolur.

exclusive: Kuyruğun özel olup olmadığını belirler. true olarak ayarlanırsa, kuyruk yalnızca bağlantıyı oluşturan taraf için geçerli olur ve bağlantı kapatıldığında kuyruk da silinir. false olarak ayarlanırsa, kuyruk diğer bağlantılar tarafından da kullanılabilir.

autoDelete: Kuyruğun otomatik silinip silinmeyeceğini belirler. true olarak ayarlanırsa, kuyruk son tüketicisi (kuyruğa mesaj gönderip alan) kalmadığında otomatik olarak silinir. false olarak ayarlanırsa, kuyruk manuel olarak silinmelidir. */

string message = "Hello word!";
var messageBody = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange:string.Empty,
	routingKey:"hello-queue",basicProperties:null,body:messageBody);
/* 
	exchange: Mesajın yönlendirileceği değişkenin (exchange) adıdır. Bu örnekte string.Empty olarak belirtilmiş, yani varsayılan değişken kullanılıyor. Varsayılan değişken genellikle "direct" değişken olarak adlandırılır ve mesaj doğrudan belirtilen kuyruklara yönlendirilir.

	routingKey: Mesajın yönlendirileceği kuyruk adıdır. Bu örnekte "hello-queue" olarak belirtilmiş. Routing key, mesajın hangi kuyruklara yönlendirileceğini belirler. Bu parametre, kullanılan değişkenin türüne bağlı olarak farklı anlamlara gelebilir, ancak burada basit bir durumda kuyruk adını belirtir.

	basicProperties: Mesajın özelliklerini belirten bir nesnedir. Bu parametre null olarak ayarlanmış, bu da mesajın özelliklerinin varsayılan ayarlarla gönderileceği anlamına gelir. Bu özellikler arasında içerik türü, öncelik, mesajın TTL (Time To Live) süresi ve diğer meta veriler bulunabilir.

	body: Gönderilecek mesajın içeriğidir. Bu parametre genellikle bir byte[] (bayt dizisi) olarak belirtilir. Mesajın gerçek verisi burada yer alır.

	Özetle, BasicPublish yöntemi RabbitMQ'ya mesaj gönderir.
 
 */

Console.WriteLine("Mesajınız gönderildi.");