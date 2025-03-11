using MediatR;
using Shop.Application.DTOs.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Api.Requests.Queries
{
    public record GetProductsFromWordpressQueryReq(int page):IRequest<List<resultWordpress>>;
    public record GetProductFromWordpressQueryReq(string ProductId):IRequest<List<resultWordpress>>;
    public record GetvariationsFromWordpressQueryReq():IRequest<List<resultWordpress>>;
    public record FixproductStockPriceQueryReq():IRequest<Unit>;

}
