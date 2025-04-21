using MediatR;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Queries
{
    public record FinallyOrdersOfUserByPagingQueryReq(int userId, ParameterDto Parameter):IRequest<PagingDto>;
}
