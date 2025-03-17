using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;

using System.Globalization;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Handlers.Commands;

public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommandReq, CouponDto>
{
    private readonly ICouponRepository _couponRepo;
    private readonly IMapper _mapper;

    public CreateCouponCommandHandler(ICouponRepository couponRepo, IMapper mapper)
    {
        _couponRepo = couponRepo;
        _mapper = mapper;
    }

    public async Task<CouponDto> Handle(CreateCouponCommandReq request, CancellationToken cancellationToken)
    {
        request.CouponDto.ExpireDateShamsi =
            ConvertPersianNumbersToEnglish.ToEnglishNumber(request.CouponDto.ExpireDateShamsi);

        string[] std = request.CouponDto.ExpireDateShamsi.Split('/');
        DateTime az = new(int.Parse(std[0]),
            int.Parse(std[1]),
            int.Parse(std[2]),
            new PersianCalendar()
        );
        Domain.Models.Coupon? coupon = _mapper.Map<Domain.Models.Coupon>(request.CouponDto);
        coupon.ExpireDate = az;
        Domain.Models.Coupon couponResult = await _couponRepo.AddAsync(coupon);
        return _mapper.Map<CouponDto>(couponResult);
    }
}