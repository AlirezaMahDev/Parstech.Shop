using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Handler.Queries;

public class AddOrUpdateOrderPayQueryHandler : IRequestHandler<AddOrUpdateOrderPayQueryReq, ResponseDto>
{
    private readonly IOrderPayRepository _orderPayRep;

    public AddOrUpdateOrderPayQueryHandler(IOrderPayRepository orderPayRep)
    {
        _orderPayRep = orderPayRep;
    }

    public async Task<ResponseDto> Handle(AddOrUpdateOrderPayQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        if (await _orderPayRep.HasOrderPay(request.orderId))
        {
            Shared.Models.OrderPay item = await _orderPayRep.GetByOrderId(request.orderId);
            if (request.PayTypeId != 0)
            {
                item.PayTypeId = request.PayTypeId.Value;
            }

            if (request.PayStatysId != 0)
            {
                item.PayStatusTypeId = request.PayStatysId.Value;
            }

            await _orderPayRep.UpdateAsync(item);
            response.IsSuccessed = true;
        }
        else
        {
            if (request.orderId != 0 && request.PayTypeId != 0 && request.PayStatysId != 0)
            {
                try
                {
                    Random random = new();
                    Shared.Models.OrderPay newItem = new()
                    {
                        OrderId = request.orderId,
                        PayTypeId = request.PayTypeId.Value,
                        PayStatusTypeId = request.PayStatysId.Value,
                        Description = request.Description,
                        DepositCode = random.Next(10000, 99999).ToString(),
                        Price = 0
                    };
                    await _orderPayRep.AddAsync(newItem);
                    response.IsSuccessed = true;
                }
                catch (Exception ex)
                {
                    response.IsSuccessed = false;
                }
            }
        }

        return response;
    }
}