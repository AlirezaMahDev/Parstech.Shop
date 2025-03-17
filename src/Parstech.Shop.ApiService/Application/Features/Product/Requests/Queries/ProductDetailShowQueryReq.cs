using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record ProductDetailShowQueryReq(string ShortLink, int productStockPriceId, string userName)
    : IRequest<ProductDetailShowDto>;