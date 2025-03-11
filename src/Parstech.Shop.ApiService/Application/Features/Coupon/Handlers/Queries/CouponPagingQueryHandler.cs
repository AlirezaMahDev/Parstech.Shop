using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.Paging;
using Shop.Application.Features.Coupon.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Handlers.Queries
{
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

            PagingDto response = new PagingDto();

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
}
