using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record GetProductsFromWordpressQueryReq(int page) : IRequest<List<resultWordpress>>;

public record GetProductFromWordpressQueryReq(string ProductId) : IRequest<List<resultWordpress>>;

public record GetvariationsFromWordpressQueryReq() : IRequest<List<resultWordpress>>;

public record FixproductStockPriceQueryReq() : IRequest<Unit>;