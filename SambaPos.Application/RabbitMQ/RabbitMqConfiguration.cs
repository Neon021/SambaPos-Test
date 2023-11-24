namespace SambaPos.Application.RabbitMQ;
public class RabbitMqConfiguration
{
    public const string HostName = "localhost";
    public const int Port = 5672;
    public const string UserName = "guest";
    public const string Password = "guest";

    public const string ExchangeName = "sambapos-exchange";
    public const string OrderServiceQueue = "sambapos-order-queue";
}
