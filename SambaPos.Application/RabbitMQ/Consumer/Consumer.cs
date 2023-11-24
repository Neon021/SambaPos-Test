using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SambaPos.Application.RabbitMQ.Publisher;
using System.Text;

namespace SambaPos.Application.RabbitMQ.Consumer;
public class ConsumerService : IConsumerService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<PublisherService> _logger;

    public ConsumerService(ILogger<PublisherService> logger)
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


    public Task ConsumeMessage<T>(Action<T> messageHandler)
    {
        try
        {
            _channel.ExchangeDeclare(exchange: RabbitMqConfiguration.ExchangeName, type: ExchangeType.Direct, durable: false, arguments: null);
            _channel.QueueDeclare(queue: RabbitMqConfiguration.OrderServiceQueue, exclusive: false, durable: true, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: RabbitMqConfiguration.OrderServiceQueue, exchange: RabbitMqConfiguration.ExchangeName, routingKey: RabbitMqConfiguration.OrderServiceQueue);
            _channel.BasicQos(prefetchCount: 1, prefetchSize: 0, global: false);

            AsyncEventingBasicConsumer consumer = new(_channel);

            _channel.BasicConsume(queue: RabbitMqConfiguration.OrderServiceQueue, autoAck: false, consumer: consumer);

            consumer.Received += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));
                    _logger.LogInformation($"Receiver message: {message}");

                    messageHandler(message);

                    _channel.BasicAck(eventArgs.DeliveryTag, multiple: true);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in Received Event: {ex.Message}");
                }
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in ConsumeMessage: {ex.Message}");
            CloseConnection();
        }

        return Task.CompletedTask;
    }

    public Task CloseConnection()
    {
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}
