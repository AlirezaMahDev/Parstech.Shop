using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.OrderPay.Request.Command;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Handler.Command;

public class OrderPayCreateCommandHandler : IRequestHandler<OrderPayCreateCommandReq, ResponseDto>
{
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IMapper _mapper;
    public OrderPayCreateCommandHandler(IOrderPayRepository orderPayRep, IMapper mapper)
    {
        _orderPayRep = orderPayRep;
        _mapper= mapper;
    }
    public async Task<ResponseDto> Handle(OrderPayCreateCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        var item = _mapper.Map<Domain.Models.OrderPay>(request.orderPayDto);
        Random random=new();
        item.DepositCode = random.Next(1000, 9999).ToString();
        await _orderPayRep.AddAsync(item);
        response.IsSuccessed = true;
        return response;
    }
}