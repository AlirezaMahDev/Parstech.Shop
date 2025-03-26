using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductProperty;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Queries;

public record PropertiesOfProductQueryReq(int productId) : IRequest<List<ProductPropertyDto>>;