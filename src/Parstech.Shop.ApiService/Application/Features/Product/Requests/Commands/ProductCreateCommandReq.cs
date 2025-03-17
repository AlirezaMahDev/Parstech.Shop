using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

public record ProductCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;

public record ProductWordpressCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;