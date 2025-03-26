using MediatR;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record UpdateVariationNameOfProductQueryReq(int productId,string VariationName):IRequest<bool>;