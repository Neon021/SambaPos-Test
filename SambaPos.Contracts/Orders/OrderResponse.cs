namespace SambaPos.Contracts.Orders;
public record OrderResponse(
    string Id,
    string Name,
    string Description,
    List<OrderContentResponse> Contents,
    string HostId,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime);

public record OrderContentResponse(
    string Id,
    string Name,
    string Description);