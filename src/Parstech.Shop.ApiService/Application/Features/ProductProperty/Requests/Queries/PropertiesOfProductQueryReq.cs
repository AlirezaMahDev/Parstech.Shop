using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Queries;

public record PropertiesOfProductQueryReq(int productId) : IRequest<List<ProductPropertyDto>>;