using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Coupons;

[Authorize(Roles = "SupperUser,Sale,Finanicial")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly CouponAdminGrpcClient _couponAdminClient;

    public IndexModel(CouponAdminGrpcClient couponAdminClient)
    {
        _couponAdminClient = couponAdminClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ParameterDto Parameter { get; set; } = new ParameterDto();

    [BindProperty]
    public PagingDto List { get; set; }

    [BindProperty]
    public List<CouponDto> Coupons { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public List<CouponTypeDto> couponTypes { get; set; }

    [BindProperty]
    public CouponDto couponDto { get; set; }

    [BindProperty]
    public int CouponId { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        // Use gRPC client to get coupon types
        var couponTypesResponse = await _couponAdminClient.GetCouponTypesAsync();

        // Map gRPC response to application DTO
        couponTypes = couponTypesResponse.Types.Select(t => MapToCouponTypeDto(t)).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;

        // Use gRPC client to get coupon paging data
        var couponsResponse = await _couponAdminClient.GetCouponsForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(couponsResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;

        // Use gRPC client to get coupon paging data
        var couponsResponse = await _couponAdminClient.GetCouponsForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(couponsResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;

        // Use gRPC client to get coupon paging data
        var couponsResponse = await _couponAdminClient.GetCouponsForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(couponsResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Create Update Delete

    public async Task<IActionResult> OnPostGetCoupon()
    {
        // Use gRPC client to get coupon by ID
        var coupon = await _couponAdminClient.GetCouponByIdAsync(CouponId);

        // Map gRPC response to application DTO
        couponDto = MapToCouponDto(coupon);
        Response.Object = couponDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostUpdateAndCreateCoupon()
    {
        var validator = new CouponDtoValidator();
        var valid = validator.Validate(couponDto);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = couponDto;
            return new JsonResult(Response);
        }

        if (couponDto.Id == 0)
        {
            // Map application DTO to gRPC DTO
            var couponGrpc = MapToGrpcCouponDto(couponDto);

            // Use gRPC client to create coupon
            var response = await _couponAdminClient.CreateCouponAsync(couponGrpc);

            Response.Object = couponDto;
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;

            if (response.Errors.Count > 0)
            {
                Response.Errors = response.Errors.Select(e =>
                        new FluentValidation.Results.ValidationFailure(e.PropertyName, e.ErrorMessage))
                    .ToList();
            }

            return new JsonResult(Response);
        }
        else
        {
            // Map application DTO to gRPC DTO
            var couponGrpc = MapToGrpcCouponDto(couponDto);

            // Use gRPC client to update coupon
            var response = await _couponAdminClient.UpdateCouponAsync(couponGrpc);

            Response.Object = couponDto;
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;

            if (response.Errors.Count > 0)
            {
                Response.Errors = response.Errors.Select(e =>
                        new FluentValidation.Results.ValidationFailure(e.PropertyName, e.ErrorMessage))
                    .ToList();
            }

            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostDeleteCoupon()
    {
        // Use gRPC client to delete coupon
        var response = await _couponAdminClient.DeleteCouponAsync(CouponId);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;

        return new JsonResult(Response);
    }

    #endregion

    public async Task<IActionResult> OnPostTest()
    {
        return Page();
    }

    #region Mapping Methods

    private PagingDto MapToPagingDto(Shop.Shared.Protos.CouponAdmin.CouponPageingDto dto)
    {
        var result = new PagingDto { CurrentPage = dto.CurrentPage, PageCount = dto.PageCount };

        result.List = dto.List.Select(MapToCouponDto).ToList();

        return result;
    }

    private CouponDto MapToCouponDto(Shop.Shared.Protos.CouponAdmin.CouponDto dto)
    {
        return new CouponDto
        {
            Id = dto.Id,
            Code = dto.Code,
            Amount = dto.Amount,
            Persent = dto.Persent,
            MinPrice = dto.MinPrice,
            MaxPrice = dto.MaxPrice,
            LimitUse = dto.LimitUse,
            LimitEachUser = dto.LimitEachUser,
            CouponTypeId = dto.CouponTypeId,
            ExpireDateShamsi = dto.ExpireDateShamsi,
            Categury = dto.Categury,
            Products = dto.Products,
            Users = dto.Users,
            TwoUseSameTime = dto.TwoUseSameTime,
            JustNewUser = dto.JustNewUser
        };
    }

    private Parstech.Shop.Shared.Protos.CouponAdmin.CouponDto MapToGrpcCouponDto(CouponDto coupon)
    {
        return new Shop.Shared.Protos.CouponAdmin.CouponDto
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

    private CouponTypeDto MapToCouponTypeDto(Shop.Shared.Protos.CouponAdmin.CouponTypeDto dto)
    {
        return new CouponTypeDto { Id = dto.Id, Type = dto.Type };
    }

    #endregion
}