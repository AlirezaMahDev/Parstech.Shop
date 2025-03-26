using System.Globalization;

using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Coupon;
using Parstech.Shop.Context.Application.Features.Coupon.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Coupon.Handlers.Commands;

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
        request.CouponDto.ExpireDateShamsi = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.CouponDto.ExpireDateShamsi);

        string[] std = request.CouponDto.ExpireDateShamsi.Split('/');
        var az = new DateTime(int.Parse(std[0]),
            int.Parse(std[1]),
            int.Parse(std[2]),
            new PersianCalendar()
        );
        var coupon = _mapper.Map<Domain.Models.Coupon>(request.CouponDto);
        coupon.ExpireDate = az;
        var couponResult = await _couponRepo.AddAsync(coupon);
        return _mapper.Map<CouponDto>(couponResult);
    }
}