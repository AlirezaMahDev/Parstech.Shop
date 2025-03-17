using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductType.Requests.Commands;

public record ProductTypeReadCommandReq(int id) : IRequest<ProductTypeDto>;

public record ProductTypeReadsCommandReq() : IRequest<List<ProductTypeDto>>;