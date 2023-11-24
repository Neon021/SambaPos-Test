using Mapster;
using SambaPos.Application.Orders.Commands.CreateOrder;
using SambaPos.Contracts.Orders;
using SambaPos.Domain.Hosts.ValueObjects;
using SambaPos.Domain.Orders;
using SambaPos.Domain.Orders.Entities;

namespace SambaPos.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateOrderRequest Request, HostId HostId), CreateOrderCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.HostId, src => src.HostId.Value);

        config.NewConfig<OrderContent, OrderContentResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}