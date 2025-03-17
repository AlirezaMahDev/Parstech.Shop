using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

public record ProductUpdateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;