using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.OrderPay;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.OrderPay.Request.Queries;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Handler.Queries;

public class OrderPaysOfOrderQueryHandler : IRequestHandler<OrderPaysOfOrderQueryReq, ResponseDto>
{
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IPayTypeRepository _payTypeRep;
    private readonly IMapper _mapper;
    public OrderPaysOfOrderQueryHandler(IOrderPayRepository orderPayRep, IMapper mapper, IPayTypeRepository payTypeRep)
    {
        _orderPayRep=orderPayRep;
        _mapper=mapper;
        _payTypeRep=payTypeRep;
    }
    public async Task<ResponseDto> Handle(OrderPaysOfOrderQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response=new();
        var list =await _orderPayRep.GetListByOrderId(request.orderId);
        List<OrderPayDto> listDto = new();
        foreach (var item in list)
        {
            var dto=_mapper.Map<OrderPayDto>(item);
            var payType =await _payTypeRep.GetAsync(item.PayTypeId);
            dto.TypeName = payType.TypeName;
            listDto.Add(dto);
        }
        response.IsSuccessed = true;
        response.Object = listDto;
        return response;
    }
}