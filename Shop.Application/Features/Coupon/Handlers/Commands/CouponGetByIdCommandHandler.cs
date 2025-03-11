using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.Coupon;
using Shop.Application.Features.Coupon.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Handlers.Commands
{
    public class CouponGetByIdCommandHandler : IRequestHandler<CouponGetByIdCommandReq, CouponDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICouponRepository _couponRepo;
        public CouponGetByIdCommandHandler(ICouponRepository couponRepo, IMapper mapper, IMediator mediator)
        {
            _couponRepo = couponRepo;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<CouponDto> Handle(CouponGetByIdCommandReq request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepo.GetAsync(request.couponId);
            var couponDto = _mapper.Map<CouponDto>(coupon);
            couponDto.ExpireDateShamsi = coupon.ExpireDate.ToShamsi();
            return couponDto;
        }
    }
}
