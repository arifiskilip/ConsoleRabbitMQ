using RabbitMQ.Client;

namespace ConsoleRabbitMQ.Watermark.Services
{
	public class RabbitMQClientService : IDisposable
	{
		private readonly ConnectionFactory _factory;
		private readonly IConnection _connection;
		private IModel _channel;
		private readonly ILogger<RabbitMQClientService> _logger;

		public string ExchangeName = "ImageDirrectExchange";
		public string RoutingWatermark = "watermark-route-image";
		public string QueueName = "queue-watermark-image";

		public RabbitMQClientService(ConnectionFactory factory, ILogger<RabbitMQClientService> logger)
		{
			_factory = factory;
			_logger = logger;
		}

		public IModel Connect()
		{
			if (_channel is { IsOpen: true }) { return _channel; }

			_channel = _connection.CreateModel();
			_channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true, false);
			_channel.QueueDeclare(QueueName, true, false, false, null);
			_channel.QueueBind(QueueName,ExchangeName,RoutingWatermark);

			_logger.LogInformation("Created new channel");
			return _channel;

		}
		public void Dispose()
		{
			_channel?.Dispose();
			_channel?.Close();

			_connection?.Dispose();
			_connection?.Close();
		}
	}
}
