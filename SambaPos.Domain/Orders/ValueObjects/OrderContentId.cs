using SambaPos.Domain.Common;

namespace SambaPos.Domain.Orders.ValueObjects;
public sealed class OrderContentId : ValueObject
{
    public Guid Value { get; }

    private OrderContentId(Guid value)
    {
        Value = value;
    }

    public static OrderContentId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static OrderContentId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private OrderContentId() { }
}