using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Handler.Commands;

public class OrderDetailReadCommandHandler : IRequestHandler<OrderDetailReadCommandReq, OrderDetailDto>
{
    private IOrderDetailRepository _orderDetailRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public OrderDetailReadCommandHandler(IOrderDetailRepository orderDetailRep, IMapper mapper, IMediator mediator)
    {
        _orderDetailRep= orderDetailRep;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<OrderDetailDto> Handle(OrderDetailReadCommandReq request, CancellationToken cancellationToken)
    {
        var item = await _orderDetailRep.GetAsync(request.id);
        return _mapper.Map<OrderDetailDto>(item);
    }
}