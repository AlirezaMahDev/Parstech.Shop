using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Requests.Queries
{
    public record ProductStockPriceDeleteQueryReq(int repId,int productStockPriceId):IRequest<bool>;

}
