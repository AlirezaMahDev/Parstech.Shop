using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

public record ProductCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;

public record ProductWordpressCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;