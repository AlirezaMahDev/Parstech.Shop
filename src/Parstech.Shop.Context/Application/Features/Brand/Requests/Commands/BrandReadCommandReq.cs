using MediatR;

using Parstech.Shop.Context.Application.DTOs.Brand;

namespace Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;

public record BrandReadCommandReq(int id) : IRequest<BrandDto>;
public record BrandReadsCommandReq() : IRequest<List<BrandDto>>;