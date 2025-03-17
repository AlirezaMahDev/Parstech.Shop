using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record ProductEditContentQueryReq(int productId, string content) : IRequest<ProductDto>;