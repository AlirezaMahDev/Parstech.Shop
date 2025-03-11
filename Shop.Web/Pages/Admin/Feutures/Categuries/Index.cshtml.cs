using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.PropertyCategury.Requests.Commands;
using Shop.Application.Features.PropertyCategury.Requests.Queries;

namespace Shop.Web.Pages.Admin.Feutures.Categuries
{
    [Authorize(Roles = "SupperUser,Sale")]
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

        //paging parameter
        [BindProperty]
        public ParameterDto Parameter { get; set; } = new ParameterDto();

        //categuries
        [BindProperty]
        public PagingDto List { get; set; }

        //categury
        [BindProperty]
        public PropertyCateguryDto PropertyCateguryDto { get; set; }

        [BindProperty]
        public int PropertycateguryId { get; set; }

        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {
            Parameter.CurrentPage = 1;
            Parameter.TakePage = 30;
            List = await _mediator.Send(new PropertyCateguryPagingQueryReq(Parameter));
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
            List = await _mediator.Send(new PropertyCateguryPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostPaging()
        {
            Parameter.TakePage = 30;
            List = await _mediator.Send(new PropertyCateguryPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region Add Or EditCategury

        public async Task<IActionResult> OnPostCategury()
        {
            PropertyCateguryDto = await _mediator.Send(new PropertyCateguryReadCommandReq(PropertycateguryId));
            Response.Object = PropertyCateguryDto;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostEditOrCreate()
        {
            if (PropertyCateguryDto.Id != 0)
            {
                await _mediator.Send(new PropertyCateguryUpdateCommandReq(PropertyCateguryDto));
                Response.Object = PropertyCateguryDto;
                Response.IsSuccessed = true;
                Response.Message = "دسته بندی با موفقیت ویرایش شد";
                return new JsonResult(Response);
            }
            else
            {
                await _mediator.Send(new PropertyCateguryCreateCommandReq(PropertyCateguryDto));
                Response.Object = PropertyCateguryDto;
                Response.IsSuccessed = true;
                Response.Message = "دسته بندی با موفقیت ثبت شد";
                return new JsonResult(Response);
            }
        }
        #endregion
    }
}
