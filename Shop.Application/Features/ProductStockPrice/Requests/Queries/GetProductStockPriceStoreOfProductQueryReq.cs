using MediatR;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Requests.Queries
{
    public record GetProductStockPriceStoreOfProductQueryReq(int ProductId) : IRequest<List<ProductStockPriceStoreDto>>;
}
