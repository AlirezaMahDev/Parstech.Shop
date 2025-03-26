using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.OrderPay.Request.Command;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Handler.Command;

public class OrderPayDeleteCommandHandler : IRequestHandler<OrderPayDeleteCommandReq, ResponseDto>
{
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IOrderRepository _orderRep;
    public OrderPayDeleteCommandHandler(IOrderPayRepository orderPayRep, IOrderRepository orderRep)
    {
        _orderPayRep = orderPayRep;
        _orderRep = orderRep;
    }
    public async Task<ResponseDto> Handle(OrderPayDeleteCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response=new();
        var item =await _orderPayRep.GetAsync(request.id);
        var order =await _orderRep.GetAsync(item.OrderId);
        if (!order.IsFinaly)
        {
            await _orderPayRep.DeleteAsync(item);
            response.IsSuccessed = true;
            response.Message = "روش پرداخت با موفقیت حذف گردید";
        }
        else
        {
            response.IsSuccessed = false;
            response.Message = "سفارش تکمیل شده امکان حذف وش پرداخت ندارد";
        }
			
        return response;
    }
}