using Mapster;
using SambaPos.Contracts.Orders;
using SambaPos.Domain.Orders;
using SambaPos.Domain.Orders.Entities;

namespace SambaPos.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<OrderContent, OrderContentResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}