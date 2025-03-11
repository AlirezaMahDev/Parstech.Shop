using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.OrderPay.Request.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderPay.Handler.Queries
{
    public class AddOrUpdateOrderPayQueryHandler : IRequestHandler<AddOrUpdateOrderPayQueryReq, ResponseDto>
    {
        private readonly IOrderPayRepository _orderPayRep;
        public AddOrUpdateOrderPayQueryHandler(IOrderPayRepository orderPayRep)
        {
            _orderPayRep = orderPayRep;
        }
        public async Task<ResponseDto> Handle(AddOrUpdateOrderPayQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto response=new ResponseDto();
            if (await _orderPayRep.HasOrderPay(request.orderId))
            {
                var item=await _orderPayRep.GetByOrderId(request.orderId);
                if(request.PayTypeId != 0) 
                {
                    item.PayTypeId = request.PayTypeId.Value;
                }
                if(request.PayStatysId != 0) 
                {
                    item.PayStatusTypeId = request.PayStatysId.Value;
                }
                await _orderPayRep.UpdateAsync(item);
                response.IsSuccessed = true;
            }
            else
            {

                if (request.orderId != 0 && request.PayTypeId != 0 && request.PayStatysId!= 0){
                    try
                    {
                        Random random = new Random();
                        Domain.Models.OrderPay newItem = new Domain.Models.OrderPay()
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
                    catch(Exception ex)
                    {
                        response.IsSuccessed = false;
                    }
                }
            }
            return response;
        }
    }
}
