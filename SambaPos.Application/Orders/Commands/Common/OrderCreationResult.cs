using SambaPos.Domain.Orders;

namespace SambaPos.Application.Orders.Commands.Common;
public record OrderCreationResult(
    Order order);
