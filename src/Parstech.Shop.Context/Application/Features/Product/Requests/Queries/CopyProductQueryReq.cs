using MediatR;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record CopyProductQueryReq(int productId, bool related):IRequest;