using SambaPos.Domain.Orders;

namespace SambaPos.Application.Common.Interfaces.Persistance;
public interface IOrderRepository
{
    void Add(Order order);
}
