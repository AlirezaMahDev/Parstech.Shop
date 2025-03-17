using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Handler.Commands;

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
        Domain.Models.OrderDetail? item = _mapper.Map<Domain.Models.OrderDetail>(request.OrderDetailDto);

        Domain.Models.OrderDetail? Result = await _orderDetailRep.UpdateAsync(item);
        return _mapper.Map<OrderDetailDto>(Result);
    }
}