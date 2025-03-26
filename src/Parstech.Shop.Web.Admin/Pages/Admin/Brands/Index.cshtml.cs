using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Brands;

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
    public ParameterDto Parameter { get; set; } = new();

    //categuries
    [BindProperty]
    public PagingDto List { get; set; }

    //brand
    [BindProperty]
    public BrandDto BrandDto { get; set; }

    [BindProperty]
    public int BrandId { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();



    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {

        Parameter.TakePage = 30;
        List = await _mediator.Send(new BrandsPagingQueryReq(Parameter));
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
        List = await _mediator.Send(new BrandsPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        List = await _mediator.Send(new BrandsPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
    #region Add Or EditCategury

    public async Task<IActionResult> OnPostItem()
    {
        BrandDto = await _mediator.Send(new BrandReadCommandReq(BrandId));
        Response.Object = BrandDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreate()
    {
        if (BrandDto.BrandId != 0)
        {
            BrandDto.ChangeByUserName = User.Identity.Name;
            BrandDto.LastChangeTime = DateTime.Now;
            BrandDto.IsDelete = false;
            await _mediator.Send(new BrandUpdateCommandReq(BrandDto));
            Response.Object = BrandDto;
            Response.IsSuccessed = true;
            Response.Message = "برند با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            BrandDto.ChangeByUserName = User.Identity.Name;
            BrandDto.LastChangeTime = DateTime.Now;
            BrandDto.IsDelete = false;
            await _mediator.Send(new BrandCreateCommandReq(BrandDto));
            Response.Object = BrandDto;
            Response.IsSuccessed = true;
            Response.Message = "برند با موفقیت ثبت شد";
            return new JsonResult(Response);
        }
    }
    #endregion
}