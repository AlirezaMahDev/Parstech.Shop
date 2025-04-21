using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Coupon;
using Shop.Application.Features.Coupon.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Handlers.Commands
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommandReq, CouponDto>
    {
        private readonly ICouponRepository _couponRepo;
        private readonly IMapper _mapper;

        public UpdateCouponCommandHandler(ICouponRepository couponRepo, IMapper mapper)
        {
            _couponRepo = couponRepo;
            _mapper = mapper;
        }
        public async Task<CouponDto> Handle(UpdateCouponCommandReq request, CancellationToken cancellationToken)
        {
            request.CouponDto.ExpireDateShamsi = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.CouponDto.ExpireDateShamsi);

            string[] std = request.CouponDto.ExpireDateShamsi.Split('/');
			var az = new DateTime(int.Parse(std[0]),
				int.Parse(std[1]),
				int.Parse(std[2]),
				new PersianCalendar()
			);
            var coupon = _mapper.Map<Domain.Models.Coupon>(request.CouponDto);
            coupon.ExpireDate = az;
            var couponResult = await _couponRepo.UpdateAsync(coupon);
            return _mapper.Map<CouponDto>(couponResult);
        }
    }
}
