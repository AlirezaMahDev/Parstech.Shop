using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;

public record BrandUpdateCommandReq(BrandDto BrandDto) : IRequest<BrandDto>;