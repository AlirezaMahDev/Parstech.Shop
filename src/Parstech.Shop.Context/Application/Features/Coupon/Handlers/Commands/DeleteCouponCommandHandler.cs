using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Coupon.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Coupon.Handlers.Commands;

public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommandReq, bool>
{
    private readonly ICouponRepository _couponRepo;
    private readonly IMapper _mapper;
    private readonly IOrderCouponRepository _orderCouponRepo;

    public DeleteCouponCommandHandler(ICouponRepository couponRepo, IMapper mapper,
        IOrderCouponRepository orderCouponRepo)
    {
        _couponRepo = couponRepo;
        _mapper = mapper;
        _orderCouponRepo = orderCouponRepo;
    }

    public async Task<bool> Handle(DeleteCouponCommandReq request, CancellationToken cancellationToken)
    {
        var isCouponExistInOrder = await _orderCouponRepo.CouponExistInOrderCoupon(request.couponId);
        if (!isCouponExistInOrder)
        {
            var coupon = await _couponRepo.GetAsync(request.couponId);
            await _couponRepo.DeleteAsync(_mapper.Map<Domain.Models.Coupon>(coupon));
            return true;
        }
        return false;
    }
}