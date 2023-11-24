using ErrorOr;
using MediatR;
using SambaPos.Application.Orders.Commands.Common;

namespace SambaPos.Application.Orders.Commands.CreateOrder;
public record CreateOrderCommand(
    //HostId HostId,
    string Name,
    string Description,
    List<CreateOrderContentsCommand> Contents) : IRequest<ErrorOr<OrderCreationResult>>;

public record CreateOrderContentsCommand(
    string Name,
    string Description);