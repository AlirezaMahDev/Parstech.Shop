using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;

namespace Shop.Application.Features.ProductLog.Requests.Queries
{
    public record PriceConflictsCreateLogQueryReq(string userName, ProductStockPriceDto crrentProduct, ProductStockPriceDto EditProduct) : IRequest<Unit>;

}
