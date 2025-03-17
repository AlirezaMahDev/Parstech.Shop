using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record GetProductByShortLinkQueryReq(string shortLink) : IRequest<ProductDto>;