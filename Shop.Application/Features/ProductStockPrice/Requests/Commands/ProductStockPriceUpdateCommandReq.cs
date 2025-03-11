using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.User;

namespace Shop.Application.Features.ProductStockPrice.Requests.Commands
{
    public record ProductStockPriceUpdateCommandReq(ProductStockPriceDto ProductStockPriceDto) : IRequest<ProductStockPriceDto>;
}
