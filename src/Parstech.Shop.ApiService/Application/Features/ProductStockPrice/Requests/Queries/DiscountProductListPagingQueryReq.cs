using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Requests.Queries
{
    public record DiscountProductListPagingQueryReq(ProductDiscountParameterDto parameter) :IRequest<ProductDiscountPagingDto>;

}
