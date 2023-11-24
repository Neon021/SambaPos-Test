using ErrorOr;
using MediatR;
using SambaPos.Application.Common.Interfaces.Persistance;
using SambaPos.Application.Orders.Commands.Common;
using SambaPos.Application.RabbitMQ.Consumer;
using SambaPos.Domain.Orders;
using SambaPos.Domain.Orders.Entities;
using SambaPos.Domain.Orders.ValueObjects;

namespace SambaPos.Application.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<OrderCreationResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPublisher _publisher;
    private readonly IConsumerService _consumer;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IPublisher publisher, IConsumerService consumer)
    {
        _orderRepository = orderRepository;
        _publisher = publisher;
        _consumer = consumer;
    }

    public async Task<ErrorOr<OrderCreationResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(request);

        // Assuming ConsumeMessage is an async operation
        var orderCreationResult = await _consumer.ConsumeMessage<CreateOrderCommand, OrderCreationResult>(async (message) =>
        {
            var order = Order.Create(
                name: message.Name,
                description: message.Description,
                contents: message.Contents.ConvertAll(contents => OrderContent.Create(
                    name: contents.Name,
                    description: contents.Description)));

            _orderRepository.Add(order);
            return new OrderCreationResult(order);
        });

        return orderCreationResult;
        //await _publisher.Publish(request);

        //await _consumer.ConsumeMessage<CreateOrderCommand>(async (message) =>
        //{
        //    var order = Order.Create(
        //        //hostId: request.HostId,
        //        name: request.Name,
        //        description: request.Description,
        //        contents: request.Contents.ConvertAll(contents => OrderContent.Create(
        //        name: contents.Name,
        //        description: contents.Description)));

        //    _orderRepository.Add(order);
        //    return new OrderCreationResult(order);
        //});

        //return new();
    }
}