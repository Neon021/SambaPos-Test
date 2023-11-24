using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SambaPos.Application.Orders.Commands.CreateOrder;
using SambaPos.Application.Orders.Common;
using SambaPos.Contracts.Orders;

namespace SambaPos.Api.Controllers;

[Route("/order")]
public class OrderController : ApiController
{
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }
    [HttpPost]
    public async Task<IActionResult> CreateMenu(
        CreateOrderRequest request,
        string hostId)
    {
        CreateOrderCommand command = _mapper.Map<CreateOrderCommand>(request);
        ErrorOr<OrderCreationResult> orderResult = await _mediatr.Send(command);

        return orderResult.Match(
            authResult => Ok(_mapper.Map<OrderResponse>(authResult)),
            errors => Problem(errors));
    }
}
