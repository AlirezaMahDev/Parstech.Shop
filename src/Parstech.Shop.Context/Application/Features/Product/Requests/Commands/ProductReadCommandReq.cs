using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Commands;

public record ProductReadCommandReq(int id) : IRequest<ProductDto>;