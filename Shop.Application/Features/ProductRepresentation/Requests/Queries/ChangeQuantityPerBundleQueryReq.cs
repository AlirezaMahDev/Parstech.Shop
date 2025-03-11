using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRepresentation.Requests.Queries
{
    public record ChangeQuantityPerBundleQueryReq(int productStockPriceId,int QuantityPerBundle):IRequest<bool>;
    
}
