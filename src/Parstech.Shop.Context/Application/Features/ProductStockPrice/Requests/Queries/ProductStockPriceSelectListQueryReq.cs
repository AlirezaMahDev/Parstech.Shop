using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceSelectListQueryReq(int repId):IRequest<List<ProductSelectDto>>;