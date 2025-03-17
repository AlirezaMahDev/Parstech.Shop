using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record DiscountProductListPagingQueryReq(ProductDiscountParameterDto parameter)
    : IRequest<ProductDiscountPagingDto>;