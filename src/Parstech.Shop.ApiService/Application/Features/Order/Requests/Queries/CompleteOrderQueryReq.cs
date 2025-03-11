using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Queries
{
    public record CompleteOrderQueryReq(int orderId,int orderShippingId,int payTypeId,int? transactionId,int? month) :IRequest<ResponseDto>;
}
