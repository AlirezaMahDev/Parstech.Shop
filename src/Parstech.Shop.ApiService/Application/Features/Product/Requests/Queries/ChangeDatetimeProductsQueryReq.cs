using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record ChangeDatetimeProductsQueryReq() : IRequest<Unit>;