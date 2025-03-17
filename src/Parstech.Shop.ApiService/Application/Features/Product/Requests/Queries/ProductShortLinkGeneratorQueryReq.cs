using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record ProductShortLinkGeneratorQueryReq : IRequest<string>;