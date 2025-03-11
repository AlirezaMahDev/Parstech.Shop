using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductLog;

namespace Shop.Application.Features.ProductLog.Requests.Queries
{
    public record ProductLogReadsFromProductIdQueryReq(int productId) : IRequest<LogDto>;
}
