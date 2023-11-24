using SambaPos.Domain.Common;
using SambaPos.Domain.Orders.ValueObjects;

namespace SambaPos.Domain.Orders.Entities;
public sealed class OrderContent : Entity<OrderContentId>
{
    public string Name { get; }
    public string Description { get; }

    private OrderContent(OrderContentId menuItemId, string name, string description)
        : base(menuItemId)
    {
        Name = name;
        Description = description;
    }

    public static OrderContent Create(string name, string description)
    {
        return new(OrderContentId.CreateUnique(), name, description);
    }

#pragma warning disable CS8618
    private OrderContent() { }
#pragma warning restore CS8618
}