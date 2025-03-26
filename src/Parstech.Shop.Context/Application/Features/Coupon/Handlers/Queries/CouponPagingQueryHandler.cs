using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Coupon;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.Features.Coupon.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Coupon.Handlers.Queries;

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
        var coupons = await _couponRepo.GetAll();
        IList<CouponDto> couponDto = new List<CouponDto>();


        foreach (var coupon in coupons)
        {
            var couponD = _mapper.Map<CouponDto>(coupon);
            couponD.ExpireDateShamsi = coupon.ExpireDate.ToShamsi();
            var couponTypeName = await _couponTypeRepo.GetAsync(coupon.CouponTypeId);
            couponD.CouponTypeName = couponTypeName.Type;
            couponDto.Add(couponD);

        }

        IQueryable<CouponDto> result = couponDto.AsQueryable();

        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.parameter.Filter))
        {
            result = result.Where(p =>
                (p.Code.Contains(request.parameter.Filter)));
        }
        int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;

        response.CurrentPage = request.parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.parameter.TakePage;


        response.List = result.Skip(skip).Take(request.parameter.TakePage).ToArray();

        return response;
    }
}