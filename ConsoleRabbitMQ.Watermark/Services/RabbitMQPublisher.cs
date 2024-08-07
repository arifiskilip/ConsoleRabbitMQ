using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ConsoleRabbitMQ.Watermark.Services
{
	public class RabbitMQPublisher
	{
		private readonly RabbitMQClientService _service;

		public RabbitMQPublisher(RabbitMQClientService service)
		{
			_service = service;
		}

		public void Publish(productImageCreatedEvent createdEvent)
		{
			var channel = _service.Connect();
			var bodyString = JsonSerializer.Serialize(createdEvent);
			var bodyByte = Encoding.UTF8.GetBytes(bodyString);
			
			var properties = channel.CreateBasicProperties();
			properties.Persistent = true;
			channel.BasicPublish(exchange: _service.ExchangeName, routingKey: _service.RoutingWatermark, basicProperties: properties, body: bodyByte);
		}
	}
}
