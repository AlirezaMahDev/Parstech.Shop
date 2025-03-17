using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record ProductEditContentQueryReq(int productId, string content) : IRequest<ProductDto>;