using MediatR;

using Parstech.Shop.Context.Application.DTOs.Brand;

namespace Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;

public record BrandCreateCommandReq(BrandDto BrandDto) : IRequest<BrandDto>;