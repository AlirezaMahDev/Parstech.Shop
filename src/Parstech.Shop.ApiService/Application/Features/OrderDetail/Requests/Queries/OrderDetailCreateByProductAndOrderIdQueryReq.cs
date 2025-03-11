using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Requests.Queries
{
    public record OrderDetailCreateByProductAndOrderIdQueryReq(int orderId,int productId,string userName):IRequest<ResponseDto>;
}
