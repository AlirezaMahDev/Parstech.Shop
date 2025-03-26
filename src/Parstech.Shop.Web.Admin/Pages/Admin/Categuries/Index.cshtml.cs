using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Categuries;

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

    //categury
    [BindProperty]
    public CateguryDto CateguryDto { get; set; }

    //Parent categury
    [BindProperty]
    public List<CateguryDto> Parents { get; set; }


    [BindProperty]
    public int categuryId { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty] public string FilterCat { get; set; }
    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        Parents = await _mediator.Send(new CateguryParentsReadQueryReq());
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        //Parameter.CurrentPage = 1;
        Parameter.TakePage = 300;
        List = await _mediator.Send(new CateguryPagingQueryReq(Parameter));
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
        List = await _mediator.Send(new CateguryPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        List = await _mediator.Send(new CateguryPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
    #region Add Or EditCategury

    public async Task<IActionResult> OnPostCategury()
    {
        CateguryDto = await _mediator.Send(new CateguryOneReadCommandReq(categuryId));
        Response.Object = CateguryDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreate(bool IsParent, bool ShowMenu)
    {
        CateguryDto.IsParnet = IsParent;
        CateguryDto.Show = ShowMenu;



        if (CateguryDto.GroupId != 0)
        {
            CateguryDto.Image = "05.png";
            await _mediator.Send(new CateguryUpdateCommandReq(CateguryDto));
            Response.Object = CateguryDto;
            Response.IsSuccessed = true;
            Response.Message = "دسته بندی با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            CateguryDto.Image = "05.png";
            await _mediator.Send(new CateguryCreateCommandReq(CateguryDto));
            Response.Object = CateguryDto;
            Response.IsSuccessed = true;
            Response.Message = "دسته بندی با موفقیت ثبت شد";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        var categuries = await _mediator.Send(new CateguryReadCommandReq(FilterCat));
        Response.Object = categuries;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }


    public async Task<IActionResult> OnPostDeleteCategury()
    {
        Response = await _mediator.Send(new CateguryDeleteCommandReq(categuryId));

        return new JsonResult(Response);
    }
    #endregion
}