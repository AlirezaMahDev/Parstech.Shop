using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Queries;

public record BasePropertiesOfProductQueryReq(int productId) : IRequest<List<BaseProductPropertyDto>>;