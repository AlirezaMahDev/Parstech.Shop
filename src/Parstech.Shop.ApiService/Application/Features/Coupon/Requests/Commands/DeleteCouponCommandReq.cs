using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;

public record DeleteCouponCommandReq(int couponId) : IRequest<bool>;