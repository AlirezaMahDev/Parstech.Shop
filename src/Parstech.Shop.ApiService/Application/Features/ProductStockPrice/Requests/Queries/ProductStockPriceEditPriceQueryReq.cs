using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;

namespace Shop.Application.Features.ProductStockPrice.Requests.Queries
{
    public record ProductStockPriceEditPriceQueryReq(ProductDto product) : IRequest<ProductStockPriceDto>;

}
