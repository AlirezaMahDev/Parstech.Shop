using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Handler.Commands;

public class OrderDetailReadCommandHandler : IRequestHandler<OrderDetailReadCommandReq, OrderDetailDto>
{
    private IOrderDetailRepository _orderDetailRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public OrderDetailReadCommandHandler(IOrderDetailRepository orderDetailRep, IMapper mapper, IMediator mediator)
    {
        _orderDetailRep = orderDetailRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderDetailDto> Handle(OrderDetailReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.OrderDetail? item = await _orderDetailRep.GetAsync(request.id);
        return _mapper.Map<OrderDetailDto>(item);
    }
}