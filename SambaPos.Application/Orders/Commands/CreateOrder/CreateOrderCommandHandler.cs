using ErrorOr;
using MediatR;
using SambaPos.Application.Common.Interfaces.Persistance;
using SambaPos.Application.Orders.Common;
using SambaPos.Domain.Orders;
using SambaPos.Domain.Orders.Entities;

namespace SambaPos.Application.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<OrderCreationResult>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<OrderCreationResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var order = Order.Create(
            //hostId: request.HostId,
            name: request.Name,
            description: request.Description,
            contents: request.Contents.ConvertAll(contents => OrderContent.Create(
                name: contents.Name,
                description: contents.Description)));

        _orderRepository.Add(order);

        return new OrderCreationResult(order);
    }
}