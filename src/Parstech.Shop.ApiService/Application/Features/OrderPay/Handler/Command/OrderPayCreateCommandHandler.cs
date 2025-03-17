using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Command;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Handler.Command;

public class OrderPayCreateCommandHandler : IRequestHandler<OrderPayCreateCommandReq, ResponseDto>
{
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IMapper _mapper;

    public OrderPayCreateCommandHandler(IOrderPayRepository orderPayRep, IMapper mapper)
    {
        _orderPayRep = orderPayRep;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Handle(OrderPayCreateCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        Shared.Models.OrderPay? item = _mapper.Map<Shared.Models.OrderPay>(request.orderPayDto);
        Random random = new();
        item.DepositCode = random.Next(1000, 9999).ToString();
        await _orderPayRep.AddAsync(item);
        response.IsSuccessed = true;
        return response;
    }
}