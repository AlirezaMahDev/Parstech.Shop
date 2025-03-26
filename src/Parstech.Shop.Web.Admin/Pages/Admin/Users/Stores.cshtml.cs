using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.State;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.State.Requests.Commands;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Users;

[Authorize(Roles = "SupperUser")]
public class StoresModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    private readonly IUserStoreRepository _userStorerRep;

    public StoresModel(IMediator mediator, IUserStoreRepository userStorerRep)
    {
        _mediator = mediator;
        _userStorerRep = userStorerRep;
    }

    #endregion

    #region Properties


    public List<UserDto> Users { get; set; }
    public List<SteteDto> stetes { get; set; }

    [BindProperty]
    public UserStoreDto Input { get; set; }

    [BindProperty]
    public RepresentationDto RepInput { get; set; }


    [BindProperty]
    public List<UserStoreDto> List { get; set; }


    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        Users = await _mediator.Send(new UsersGetByRoleQueryReq("Store"));
        stetes = await _mediator.Send(new StatesReadsCommandReq());
        List = await _mediator.Send(new StoreListQueryReq());
        return Page();
    }
    public async Task<IActionResult> OnGetData()
    {
        Input = await _mediator.Send(new UserStoreReadCommandReq(Id));
        return Page();
    }
    #endregion

    #region Add

    public async Task<IActionResult> OnPostData()
    {
        Input = await _mediator.Send(new UserStoreReadCommandReq(Id));
        Response.Object = Input;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostUpdateAndCreate()
    {
        if (Input.Id != 0)
        {
            var result = await _mediator.Send(new UserStoreUpdateCommandReq(Input));
            Response.IsSuccessed = true;
            Response.Message = "اطلاعات تامین کننده با موفقیت ویرایش شد";
            Response.IsSuccessed = true;
            Response.Object = result;
            return new JsonResult(Response);
        }
        else
        {
            var rep = await _mediator.Send(new RepresentationCreateCommandReq(RepInput));
            Input.RepId = rep.Id;
            Input = await _mediator.Send(new UserStoreCreateCommandReq(Input));
            Response.IsSuccessed = true;
            Response.Message = "اطلاعات تامین کننده با موفقیت ثبت شد";
            Response.IsSuccessed = true;
            Response.Object = Input;
            return new JsonResult(Response);
        }

        return Page();
    }
    #endregion
}