using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Coupon;
using Parstech.Shop.Context.Application.Features.Coupon.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Coupon.Handlers.Commands;

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