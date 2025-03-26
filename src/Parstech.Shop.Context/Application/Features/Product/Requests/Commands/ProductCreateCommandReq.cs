using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Commands;

public record ProductCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;
public record ProductWordpressCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;