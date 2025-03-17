using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record AddVariationForProductQueryReq(int productId, string variationName) : IRequest<bool>;