using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceDeleteQueryReq(int repId,int productStockPriceId):IRequest<bool>;