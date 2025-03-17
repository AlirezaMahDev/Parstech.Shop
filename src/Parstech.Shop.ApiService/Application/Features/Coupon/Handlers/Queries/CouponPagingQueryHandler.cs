using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Handlers.Queries;

public class CouponPagingQueryHandler : IRequestHandler<CouponPagingQueryReq, PagingDto>
{
    private readonly ICouponRepository _couponRepo;
    private readonly IMapper _mapper;
    private readonly ICouponTypeRepository _couponTypeRepo;

    public CouponPagingQueryHandler(ICouponRepository couponRepo,
        IMapper mapper,
        ICouponTypeRepository couponTypeRepo)
    {
        _couponRepo = couponRepo;
        _mapper = mapper;
        _couponTypeRepo = couponTypeRepo;
    }

    public async Task<PagingDto> Handle(CouponPagingQueryReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.Coupon>? coupons = await _couponRepo.GetAll();
        IList<CouponDto> couponDto = new List<CouponDto>();


        foreach (Domain.Models.Coupon coupon in coupons)
        {
            CouponDto? couponD = _mapper.Map<CouponDto>(coupon);
            couponD.ExpireDateShamsi = coupon.ExpireDate.ToShamsi();
            Domain.Models.CouponType? couponTypeName = await _couponTypeRepo.GetAsync(coupon.CouponTypeId);
            couponD.CouponTypeName = couponTypeName.Type;
            couponDto.Add(couponD);
        }

        IQueryable<CouponDto> result = couponDto.AsQueryable();

        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.parameter.Filter))
        {
            result = result.Where(p =>
                p.Code.Contains(request.parameter.Filter));
        }

        int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;

        response.CurrentPage = request.parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.parameter.TakePage;


        response.List = result.Skip(skip).Take(request.parameter.TakePage).ToArray();

        return response;
    }
}