namespace SambaPos.Application.RabbitMQ.Publisher;

public interface IPublisherService
{
    Task PublishMessage<T>(T message);
}