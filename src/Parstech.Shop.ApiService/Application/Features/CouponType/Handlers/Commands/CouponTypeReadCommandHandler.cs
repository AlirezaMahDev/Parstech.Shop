using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.CouponType.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.CouponType.Handlers.Commands;

public class CouponTypeReadCommandHandler : IRequestHandler<CouponTypeReadCommandReq, List<CouponTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICouponTypeRepository _couponTypeRepo;

    public CouponTypeReadCommandHandler(ICouponTypeRepository couponTypeRepo, IMapper mapper, IMediator mediator)
    {
        _couponTypeRepo = couponTypeRepo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<List<CouponTypeDto>> Handle(CouponTypeReadCommandReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.CouponType>? couponTypeList = await _couponTypeRepo.GetAll();
        return _mapper.Map<List<CouponTypeDto>>(couponTypeList);
    }
}