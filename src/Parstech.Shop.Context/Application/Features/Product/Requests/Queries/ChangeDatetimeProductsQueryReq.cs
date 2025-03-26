using MediatR;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record ChangeDatetimeProductsQueryReq():IRequest<Unit>;