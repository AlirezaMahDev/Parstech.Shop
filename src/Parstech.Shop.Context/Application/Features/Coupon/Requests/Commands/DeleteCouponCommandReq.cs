using MediatR;

namespace Parstech.Shop.Context.Application.Features.Coupon.Requests.Commands;

public record DeleteCouponCommandReq(int couponId) : IRequest<bool>;