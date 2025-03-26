using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.Section;
using Parstech.Shop.Context.Application.DTOs.SectionDetail;
using Parstech.Shop.Context.Application.Features.Section.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Section.Requests.Queries;
using Parstech.Shop.Context.Application.Features.SectionDetail.Requests.Commands;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser")]
public class SectionsModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    public SectionsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    [BindProperty]
    public List<SectionAndDetailsDto> List { get; set; }

    [BindProperty]
    public SectionDto Section { get; set; }

    [BindProperty]
    public int SectionId { get; set; }


    [BindProperty]
    public SectionDetailDto SectionDetail { get; set; }

    [BindProperty]
    public int SectionDeatilId { get; set; }


    [BindProperty] public ResponseDto Response { get; set; } = new();
    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        List = await _mediator.Send(new SectionAndDetailsReadsQueryReq(null));
        return Page();
    }

    //public async Task<IActionResult> OnPostGetAgain()
    //{
    //    List = await _mediator.Send(new SectionAndDetailsReadQueryReq());
    //    Response.Object = List;
    //    return new JsonResult(Response);
    //}

    public async Task<IActionResult> OnPostSection()
    {
        Section = await _mediator.Send(new SectionReadCommandReq(SectionId));
        Response.Object = Section;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostSectionDetail()
    {
        SectionDetail = await _mediator.Send(new SectionDetailReadCommandReq(SectionDeatilId));
        Response.Object = SectionDetail;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Create and Update

    public async Task<IActionResult> OnPostCreateUpdateSection()
    {
        if (SectionId != 0)
        {
            var result = await _mediator.Send(new SectionUpdateCommandReq(Section));
            Response.Object = result;
            Response.IsSuccessed = true;
            Response.Message = "ویرایش با موفقیت انجام شد";
            return new JsonResult(Response);
        }
        else
        {
            var result = await _mediator.Send(new SectionCreateCommandReq(Section));
            Response.Object = result;
            Response.IsSuccessed = true;
            Response.Message = "آیتم جدید با موفقیت افزوده شد";
            return new JsonResult(Response);
        }

    }

    public async Task<IActionResult> OnPostCreateUpdateSectionDetail()
    {

        if (SectionDeatilId != 0)
        {
            var result = await _mediator.Send(new SectionDetailUpdateCommandReq(SectionDetail));
            Response.Object = result;
            Response.IsSuccessed = true;
            Response.Message = "ویرایش با موفقیت انجام شد";
            return new JsonResult(Response);
        }
        else
        {
            SectionDetail.SectionId = SectionId;
            var result = await _mediator.Send(new SectionDetailCreateCommandReq(SectionDetail));
            Response.Object = result;
            Response.IsSuccessed = true;
            Response.Message = "آیتم جدید با موفقیت افزوده شد";
            return new JsonResult(Response);
        }

    }

    #endregion


    #region Delete

    public async Task<IActionResult> OnPostDeleteSection()
    {
        if (await _mediator.Send(new SectionCheckQueryReq(SectionId)))
        {
            await _mediator.Send(new SectionDeleteCommandReq(SectionId));
            Response.IsSuccessed = true;
            Response.Message = "آیتم با موفقیت حذف شد";
            return new JsonResult(Response);
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "تا زمانی که قسمت اصلی سایت دارای زیر مجموعه باشد امکان حذف آن وجود ندارد";
            return new JsonResult(Response);
        }


    }

    public async Task<IActionResult> OnPostDeleteSectionDetail()
    {
        var result = await _mediator.Send(new SectionDetailDeleteCommandReq(SectionDeatilId));
        Response.IsSuccessed = true;
        Response.Message = "آیتم با موفقیت حذف شد";
        return new JsonResult(Response);
    }

    #endregion

}