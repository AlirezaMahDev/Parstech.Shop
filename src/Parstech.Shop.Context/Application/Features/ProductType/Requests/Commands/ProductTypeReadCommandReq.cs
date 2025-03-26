using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductType;

namespace Parstech.Shop.Context.Application.Features.ProductType.Requests.Commands;

public record ProductTypeReadCommandReq(int id) : IRequest<ProductTypeDto>;
public record ProductTypeReadsCommandReq() : IRequest<List<ProductTypeDto>>;