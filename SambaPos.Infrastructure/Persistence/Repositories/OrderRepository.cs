using SambaPos.Application.Common.Interfaces.Persistance;
using SambaPos.Domain.Orders;

namespace SambaPos.Infrastructure.Persistence.Repositories;
public sealed class OrderRepository : IOrderRepository
{
    private readonly SambaPosDbContext _dbContext;

    public OrderRepository(SambaPosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Order order)
    {
        _dbContext.Add(order);
        _dbContext.SaveChanges();
    }
}