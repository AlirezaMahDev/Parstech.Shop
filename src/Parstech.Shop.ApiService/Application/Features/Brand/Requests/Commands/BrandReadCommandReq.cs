using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;

public record BrandReadCommandReq(int id) : IRequest<BrandDto>;

public record BrandReadsCommandReq() : IRequest<List<BrandDto>>;