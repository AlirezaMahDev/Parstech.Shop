using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Command;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Handler.Command;

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
        ResponseDto response = new();
        Domain.Models.OrderPay? item = await _orderPayRep.GetAsync(request.id);
        Domain.Models.Order? order = await _orderRep.GetAsync(item.OrderId);
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