using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Coupon;
using Shop.Application.Features.Coupon.Requests.Commands;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Handlers.Commands
{
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
}
