using MediatR;
using Shop.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Requests.Queries
{
    public record ContractOrderDetailQueryReq(Domain.Models.OrderDetail detail,string Store):IRequest<ContractDto>;

}
