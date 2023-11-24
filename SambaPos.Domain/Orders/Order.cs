using SambaPos.Domain.Common;
using SambaPos.Domain.Orders.Entities;
using SambaPos.Domain.Orders.ValueObjects;

namespace SambaPos.Domain.Orders;
public sealed class Order : AggregateRoot<OrderId, Guid>
{
    private readonly List<OrderContent> _contents;

    public string Name { get; private set; }
    public string Description { get; private set; }

    public IReadOnlyList<OrderContent> Content => _contents.AsReadOnly();

    //public HostId HostId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public Order(
        OrderId orderId,
        //HostId hostId,
        string name,
        string description,
        List<OrderContent>? contents) : base(orderId)
    {
        //HostId = hostId;
        Name = name;
        Description = description;
        _contents = contents;
    }

    public static Order Create(
        //HostId hostId,
        string name,
        string description,
        List<OrderContent>? contents = null)
    {
        Order menu = new(
            OrderId.CreateUnique(),
            //hostId,
            name,
            description,
            contents ?? new());

        return menu;
    }

#pragma warning disable CS8618
    private Order() { }
#pragma warning restore CS8618
}
