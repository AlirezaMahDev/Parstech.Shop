using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.ProductStockPrice.Requests.Commands
{
    public record ProductStockPriceDeleteCommandReq(int id) : IRequest<Unit>;
}
