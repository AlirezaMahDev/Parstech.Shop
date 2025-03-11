using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.CouponType;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Coupon.Requests.Commands;
using Shop.Application.Features.Coupon.Requests.Queries;
using Shop.Application.Features.CouponType.Requests.Commands;
using Shop.Application.Validators.Coupon;

namespace Shop.Web.Pages.Admin.Coupons
{
    [Authorize(Roles = "SupperUser,Sale,Finanicial")]
    public class IndexModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
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
            couponTypes = await _mediator.Send(new CouponTypeReadCommandReq());
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {
            Parameter.CurrentPage = 1;
            Parameter.TakePage = 30;
            List = await _mediator.Send(new CouponPagingQueryReq(Parameter));
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
            List = await _mediator.Send(new CouponPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostPaging()
        {
            Parameter.TakePage = 30;
            List = await _mediator.Send(new CouponPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region Create Update Delete

        public async Task<IActionResult> OnPostGetCoupon()
        {
            couponDto = await _mediator.Send(new CouponGetByIdCommandReq(CouponId));
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

                await _mediator.Send(new CreateCouponCommandReq(couponDto));
                Response.Object = couponDto;
                Response.IsSuccessed = true;
                Response.Message = "کوپن با موفقیت ثبت شد";
                return new JsonResult(Response);
            }
            else
            {
                await _mediator.Send(new UpdateCouponCommandReq(couponDto));
                Response.Object = couponDto;
                Response.IsSuccessed = true;
                Response.Message = "کوپن با موفقیت ویرایش شد";
                return new JsonResult(Response);
            }

        }
        public async Task<IActionResult> OnPostDeleteCoupon()
        {

            var result = await _mediator.Send(new DeleteCouponCommandReq(CouponId));
            if (!result)
            {
                Response.Object = couponDto;
                Response.IsSuccessed = false;
                Response.Message = "به دلیل وجود سفارشی با این کوپن امکان حذف وجود ندارد";
            }
            else
            {
                Response.Object = couponDto;
                Response.IsSuccessed = true;
                Response.Message = "کوپن با موفقیت حذف شد";
            }
            return new JsonResult(Response);
        }

        #endregion

        public async Task<IActionResult> OnPostTest()
        {


            return Page();
        }
    }
}
