using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

public record DiscountProductListPagingQueryReq(ProductDiscountParameterDto parameter) :IRequest<ProductDiscountPagingDto>;