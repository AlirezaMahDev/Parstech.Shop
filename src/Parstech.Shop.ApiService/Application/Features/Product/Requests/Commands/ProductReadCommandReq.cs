using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

public record ProductReadCommandReq(int id) : IRequest<ProductDto>;