using MediatR;
using Parstech.Shop.Context.Application.DTOs.CouponType;

namespace Parstech.Shop.Context.Application.Features.CouponType.Requests.Commands;

public record CouponTypeReadCommandReq() : IRequest<List<CouponTypeDto>>;