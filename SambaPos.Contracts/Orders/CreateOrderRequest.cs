namespace SambaPos.Contracts.Orders;
public record CreateOrderRequest(
    string Name,
    string Description,
    List<OrderContents> Contents);

public record OrderContents(
    string Name,
    string Description);