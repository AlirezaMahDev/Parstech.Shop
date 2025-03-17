using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

public record ProductReadCommandReq(int id) : IRequest<ProductDto>;