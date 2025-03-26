using MediatR;

using Parstech.Shop.Context.Application.DTOs.Api;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record GetProductsFromWordpressQueryReq(int page):IRequest<List<resultWordpress>>;
public record GetProductFromWordpressQueryReq(string ProductId):IRequest<List<resultWordpress>>;
public record GetvariationsFromWordpressQueryReq():IRequest<List<resultWordpress>>;
public record FixproductStockPriceQueryReq():IRequest<Unit>;