using SambaPos.Domain.Hosts.ValueObjects;
using ErrorOr;
using MediatR;
using SambaPos.Domain.Orders;

namespace SambaPos.Application.Orders.Commands.CreateOrder;
public record CreateOrderCommand(
    HostId HostId,
    string Name,
    string Description,
    List<CreateOrderContentsCommand> Contents) : IRequest<ErrorOr<Order>>;

public record CreateOrderContentsCommand(
    string Name,
    string Description);