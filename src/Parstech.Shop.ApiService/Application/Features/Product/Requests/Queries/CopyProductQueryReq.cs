using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record CopyProductQueryReq(int productId, bool related) : IRequest;