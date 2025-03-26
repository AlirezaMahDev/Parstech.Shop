using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Representations;

[Authorize(Roles = "SupperUser,Inventory,Store")]
public class QuickEditModel : PageModel
{
    #region Constractor 
    private readonly IMediator _mediator;
    private readonly IProductRepresesntationRepository _productRepresesntationRep;

    public QuickEditModel(IMediator mediator, IProductRepresesntationRepository productRepresesntationRep)
    {
        _mediator = mediator;
        _productRepresesntationRep = productRepresesntationRep;
    }
    #endregion

    #region
    public ProductRepresentationPagingDto list { get; set; }

    [BindProperty]
    public ProductRepresenationParameterDto parameters { get; set; } = new();
    public ResponseDto response { get; set; } = new();
    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }



    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetData()
    {
        parameters.TakePage = 30;
        if (parameters.CurrentPage == 0)
        {
            parameters.CurrentPage = 1;
        }

        var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
        if (userStore == null)
        {
            response.Object = null;
            response.IsSuccessed = false;
            return new JsonResult(response);
        }

        var rep = await _mediator.Send(new RepresentationReadCommandReq(userStore.RepId));
        parameters.RepId = rep.Id;
        list = await _mediator.Send(new ProductRepresentaionPagingQueryReq(parameters));
        response.Object = list;
        response.IsSuccessed = true;
        return new JsonResult(response);
    }
    #endregion
    #region SaveChanges
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostSaveChanges([FromBody] List<QuickEditDto> list)
    {

        var response = await _mediator.Send(new QuickEditProductStockPricesQueryReq(User.Identity.Name, list));
        return new JsonResult(response);
    }
    #endregion
}