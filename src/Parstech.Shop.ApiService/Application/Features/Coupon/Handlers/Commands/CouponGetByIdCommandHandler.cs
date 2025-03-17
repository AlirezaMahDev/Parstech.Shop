using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Handlers.Commands;

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
        Shared.Models.Coupon? coupon = await _couponRepo.GetAsync(request.couponId);
        CouponDto? couponDto = _mapper.Map<CouponDto>(coupon);
        couponDto.ExpireDateShamsi = coupon.ExpireDate.ToShamsi();
        return couponDto;
    }
}