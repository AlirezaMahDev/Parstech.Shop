using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Handler.Queries;

public class OrderPaysOfOrderQueryHandler : IRequestHandler<OrderPaysOfOrderQueryReq, ResponseDto>
{
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IPayTypeRepository _payTypeRep;
    private readonly IMapper _mapper;

    public OrderPaysOfOrderQueryHandler(IOrderPayRepository orderPayRep, IMapper mapper, IPayTypeRepository payTypeRep)
    {
        _orderPayRep = orderPayRep;
        _mapper = mapper;
        _payTypeRep = payTypeRep;
    }

    public async Task<ResponseDto> Handle(OrderPaysOfOrderQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        List<Shared.Models.OrderPay> list = await _orderPayRep.GetListByOrderId(request.orderId);
        List<OrderPayDto> listDto = new();
        foreach (Shared.Models.OrderPay item in list)
        {
            OrderPayDto? dto = _mapper.Map<OrderPayDto>(item);
            Shared.Models.PayType? payType = await _payTypeRep.GetAsync(item.PayTypeId);
            dto.TypeName = payType.TypeName;
            listDto.Add(dto);
        }

        response.IsSuccessed = true;
        response.Object = listDto;
        return response;
    }
}