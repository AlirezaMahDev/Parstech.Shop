using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.CouponType.Requests.Commands;

public record CouponTypeReadCommandReq() : IRequest<List<CouponTypeDto>>;