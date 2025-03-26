using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Handler.Queries;

public class OrderDetailsOfOrderQueryHandler : IRequestHandler<OrderDetailsOfOrderQueryReq, List<OrderDetailDto>>
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public OrderDetailsOfOrderQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper, IMediator mediator)
    {
        _orderDetailRepository = orderDetailRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<List<OrderDetailDto>> Handle(OrderDetailsOfOrderQueryReq request, CancellationToken cancellationToken)
    {
        var orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderId(request.orderId);
        return _mapper.Map<List<OrderDetailDto>>(orderDetails);
    }
}