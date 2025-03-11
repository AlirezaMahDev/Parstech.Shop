using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Queries
{
    
    public record CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(int orderId, int payTypeId, long price) : IRequest<ResponseDto>;
}
