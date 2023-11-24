namespace SambaPos.Application.RabbitMQ.Consumer;

public interface IConsumerService
{
    Task<TResult> ConsumeMessage<TMessage, TResult>(Func<TMessage, Task<TResult>> messageHandler);
}
