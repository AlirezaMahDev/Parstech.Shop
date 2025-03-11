using MediatR;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Requests.Queries
{
    public record ProductStockPriceSelectListQueryReq(int repId):IRequest<List<ProductSelectDto>>;

}
