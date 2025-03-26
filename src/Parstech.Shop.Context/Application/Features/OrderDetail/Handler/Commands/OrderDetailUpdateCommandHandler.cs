using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Handler.Commands;

public class OrderDetailUpdateCommandHandler : IRequestHandler<OrderDetailUpdateCommandReq, OrderDetailDto>
{

    private IOrderDetailRepository _orderDetailRep;
    private IMapper _mapper;
		

    public OrderDetailUpdateCommandHandler(IOrderDetailRepository orderDetailRep,
        IMapper mapper)
    {
        _orderDetailRep = orderDetailRep;
        _mapper = mapper;
    }

    public async Task<OrderDetailDto> Handle(OrderDetailUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Domain.Models.OrderDetail>(request.OrderDetailDto);
			
        var Result = await _orderDetailRep.UpdateAsync(item);
        return _mapper.Map<OrderDetailDto>(Result);
    }
}