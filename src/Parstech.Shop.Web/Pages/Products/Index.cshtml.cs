using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.Web.Pages.Products;

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
    public ProductSearchParameterDto Parameter { get; set; } = new();


    //products
    [BindProperty]
    public ProductPageingDto List { get; set; }


    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    //categury
    [BindProperty]
    public string Categury { get; set; }



    [BindProperty]
    public string FilterCat { get; set; }

    [BindProperty]
    public int Type { get; set; }

    [BindProperty]
    public string Filter { get; set; }


    public List<CateguryDto> categuries { get; set; }
    public List<BrandDto> Brands { get; set; }
    public List<UserStoreDto> Stores { get; set; }
    #endregion

    #region Get

    public async Task<IActionResult> OnGet(string cat)
    {
        Categury = cat;
        Brands = await _mediator.Send(new BrandReadsCommandReq());
        Stores = await _mediator.Send(new UserStoreReadsCommandReq());


        categuries = new();
        var parent = await _mediator.Send(new CateguryParentsReadQueryReq());
        foreach (var parrent in parent)
        {
            categuries.Add(parrent);
            var subParrent = await _mediator.Send(new CateguryByParentIdReadQueryReq(parrent.GroupId));
            foreach (var subP in subParrent)
            {
                categuries.Add(subP);
                var subs = await _mediator.Send(new CateguryByParentIdReadQueryReq(subP.GroupId));
                foreach (var sub in subs)
                {
                    categuries.Add(sub);

                }
            }
        }
        return Page();
    }

    public async Task<IActionResult> OnPostData(int skip, string store)
    {
        //Parameter.Skip = 0;
        Parameter.Take = 30;
        //Parameter.Type = "Active";
        #region Get User If Authenticated
        var userName = "";
        if (User.Identity.IsAuthenticated)
        {
            userName = User.Identity.Name;
        }
        else
        {
            userName = null;
        }
        #endregion'
        List = await _mediator.Send(new IntegratedProductsPagingQueryReq(Parameter,userName));
        //List = await _mediator.Send(new ProductPagingCateguryQueryReq(Parameter));
        Response.Object = List;
        var cat = await _mediator.Send(new GetCateguryByLatinameQueryReq(Parameter.Categury));
        Response.Object2 = cat;
        Response.CurrentParameter = Parameter;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
}