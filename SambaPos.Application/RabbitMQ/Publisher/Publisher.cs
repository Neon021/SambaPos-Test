using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace SambaPos.Application.RabbitMQ.Publisher;
public class PublisherService : IPublisherService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<PublisherService> _logger;

    public PublisherService(ILogger<PublisherService> logger)
    {
        _logger = logger;
        ConnectionFactory factory = new()
        {
            HostName = RabbitMqConfiguration.HostName,
            Port = RabbitMqConfiguration.Port,
            UserName = RabbitMqConfiguration.UserName,
            Password = RabbitMqConfiguration.Password,
            DispatchConsumersAsync = true
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public Task PublishMessage<T>(T message)
    {
        try
        {
            _channel.ExchangeDeclare(exchange: RabbitMqConfiguration.ExchangeName, type: ExchangeType.Direct, durable: false, autoDelete: false, arguments: null);
            byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _channel.BasicPublish(exchange: RabbitMqConfiguration.ExchangeName, routingKey: RabbitMqConfiguration.OrderServiceQueue, basicProperties: null, body: body);

            _logger.LogInformation($"Publish message: {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RabbitMq PublishMessage Error");
        }

        return Task.CompletedTask;
    }
}
