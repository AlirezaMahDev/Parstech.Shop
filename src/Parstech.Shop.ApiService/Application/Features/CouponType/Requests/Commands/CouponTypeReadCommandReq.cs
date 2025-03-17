using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.CouponType.Requests.Commands;

public record CouponTypeReadCommandReq() : IRequest<List<CouponTypeDto>>;