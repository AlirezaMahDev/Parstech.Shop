using Grpc.Core;

using MediatR;

using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.CouponType.Requests.Commands;
using Parstech.Shop.ApiService.Application.Validators.Coupon;

namespace Parstech.Shop.ApiService.Services;

public class CouponAdminGrpcService : CouponAdminService.CouponAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CouponAdminGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<CouponPageingDto> GetCouponsForAdmin(CouponParameterRequest request,
        ServerCallContext context)
    {
        ParameterDto? parameter = new()
        {
            CurrentPage = request.CurrentPage, TakePage = request.TakePage, Filter = request.Filter
        };

        void result = await _mediator.Send(new CouponPagingQueryReq(parameter));
        var response = new CouponPageingDto
        {
            CurrentPage = result.CurrentPage, PageCount = result.PageCount, RowCount = result.RowCount
        };

        foreach (var coupon in result.List)
        {
            response.List.Add(MapToCouponDto(coupon));
        }

        return response;
    }

    public override async Task<CouponDto> GetCouponById(CouponRequest request, ServerCallContext context)
    {
        void coupon = await _mediator.Send(new CouponGetByIdCommandReq(request.CouponId));
        return MapToCouponDto(coupon);
    }

    public override async Task<CouponTypesResponse> GetCouponTypes(EmptyRequest request, ServerCallContext context)
    {
        void couponTypes = await _mediator.Send(new CouponTypeReadCommandReq());
        var response = new CouponTypesResponse();

        foreach (var type in couponTypes)
        {
            response.Types.Add(MapToCouponTypeDto(type));
        }

        return response;
    }

    public override async Task<ResponseDto> CreateCoupon(CouponDto request, ServerCallContext context)
    {
        CouponDto? couponDto = MapFromCouponDto(request);

        // Validate the coupon
        var validator = new CouponDtoValidator();
        var validationResult = validator.Validate(couponDto);

        if (!validationResult.IsValid)
        {
            ResponseDto? response = new() { IsSuccessed = false, Object = request.ToString() };

            foreach (var error in validationResult.Errors)
            {
                response.Errors.Add(new ValidationError
                {
                    PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                });
            }

            return response;
        }

        await _mediator.Send(new CreateCouponCommandReq(couponDto));

        return new() { IsSuccessed = true, Message = "کوپن با موفقیت ثبت شد", Object = request.ToString() };
    }

    public override async Task<ResponseDto> UpdateCoupon(CouponDto request, ServerCallContext context)
    {
        CouponDto? couponDto = MapFromCouponDto(request);

        // Validate the coupon
        var validator = new CouponDtoValidator();
        var validationResult = validator.Validate(couponDto);

        if (!validationResult.IsValid)
        {
            ResponseDto? response = new() { IsSuccessed = false, Object = request.ToString() };

            foreach (var error in validationResult.Errors)
            {
                response.Errors.Add(new ValidationError
                {
                    PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                });
            }

            return response;
        }

        await _mediator.Send(new UpdateCouponCommandReq(couponDto));

        return new() { IsSuccessed = true, Message = "کوپن با موفقیت ویرایش شد", Object = request.ToString() };
    }

    public override async Task<ResponseDto> DeleteCoupon(CouponRequest request, ServerCallContext context)
    {
        void result = await _mediator.Send(new DeleteCouponCommandReq(request.CouponId));

        if (!result)
        {
            return new() { IsSuccessed = false, Message = "به دلیل وجود سفارشی با این کوپن امکان حذف وجود ندارد" };
        }

        return new() { IsSuccessed = true, Message = "کوپن با موفقیت حذف شد" };
    }

    private CouponDto MapToCouponDto(CouponDto coupon)
    {
        return new()
        {
            Id = coupon.Id,
            Code = coupon.Code ?? string.Empty,
            Amount = coupon.Amount,
            Persent = coupon.Persent,
            MinPrice = coupon.MinPrice,
            MaxPrice = coupon.MaxPrice,
            LimitUse = coupon.LimitUse,
            LimitEachUser = coupon.LimitEachUser,
            CouponTypeId = coupon.CouponTypeId,
            ExpireDateShamsi = coupon.ExpireDateShamsi ?? string.Empty,
            Categury = coupon.Categury ?? string.Empty,
            Products = coupon.Products ?? string.Empty,
            Users = coupon.Users ?? string.Empty,
            TwoUseSameTime = coupon.TwoUseSameTime,
            JustNewUser = coupon.JustNewUser
        };
    }

    private CouponDto MapFromCouponDto(CouponDto coupon)
    {
        return new Shop.Application.DTOs.Coupon.CouponDto
        {
            Id = coupon.Id,
            Code = coupon.Code,
            Amount = coupon.Amount,
            Persent = coupon.Persent,
            MinPrice = coupon.MinPrice,
            MaxPrice = coupon.MaxPrice,
            LimitUse = coupon.LimitUse,
            LimitEachUser = coupon.LimitEachUser,
            CouponTypeId = coupon.CouponTypeId,
            ExpireDateShamsi = coupon.ExpireDateShamsi,
            Categury = coupon.Categury,
            Products = coupon.Products,
            Users = coupon.Users,
            TwoUseSameTime = coupon.TwoUseSameTime,
            JustNewUser = coupon.JustNewUser
        };
    }

    private CouponTypeDto MapToCouponTypeDto(CouponTypeDto couponType)
    {
        return new() { Id = couponType.Id, Type = couponType.Type ?? string.Empty };
    }
}