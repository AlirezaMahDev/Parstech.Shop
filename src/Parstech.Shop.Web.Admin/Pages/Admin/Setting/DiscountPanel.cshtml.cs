using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductLog;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.DTOs.RepresentationType;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.RepresentationType.Requests.Commands;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser,Inventory,Store")]
public class DiscountPanelModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    public DiscountPanelModel(IMediator mediator)
    {
        _mediator = mediator; 
    }

    #endregion
    #region Properties

    //paging parameter
    [BindProperty]
    public ProductDiscountParameterDto Parameter { get; set; } = new();

    //categuries
    [BindProperty]
    public ProductDiscountPagingDto List { get; set; }


    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    //Representations
    [BindProperty]
    public List<RepresentationDto> Representations { get; set; } = new();


    [BindProperty]
    public int RepId { get; set; }

    [BindProperty]
    public int productId { get; set; }

    [BindProperty]
    public ProductDto product { get; set; }

    [BindProperty]
    public ProductStockPriceDto productStock { get; set; }

    [BindProperty]
    public ProductRepresentationDto productRepresentation { get; set; }

    [BindProperty]
    public List<RepresentationTypeDto> repTypes { get; set; }


    [BindProperty]
    public ProductRepresentationDto ProductRepresentationDto { get; set; }

    //log
    [BindProperty]
    public LogDto LogDto { get; set; }

    [BindProperty]
    public PagingDto ProductLogPaging { get; set; }

    [BindProperty]
    public ParameterLogDto LogParameter { get; set; } = new();

    [BindProperty]
    public ProductRepresenationParameterDto PrParameter { get; set; } = new();

    public class logClass
    {
        public LogDto LogDto { get; set; }
        public PagingDto ProductLogPaging { get; set; }
        public List<ProductRepresenationChartDto> ProductRepresntationChart { get; set; }
        public PagingDto ProductRepresntationPaging { get; set; }
    }
    [BindProperty]
    public logClass Log { get; set; } = new();
    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        Parameter.CurrentPage = 1;
        if (User.IsInRole("SupperUser"))
        {
            Representations = await _mediator.Send(new RepresentationReadsCommandReq());
        }
        else if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
            var rep = await _mediator.Send(new RepresentationReadCommandReq(userStore.RepId));
            Representations.Add(rep);


        }
        else
        {
            Representations = await _mediator.Send(new RepresentationReadsCommandReq());
        }

        repTypes = await _mediator.Send(new RepresentationTypeReadsCommandReq());
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {

        Parameter.TakePage = 30;
        List = await _mediator.Send(new DiscountProductListPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
            
        return new JsonResult(Response);
    }

        
    #endregion
    public async Task<IActionResult> OnPostGetSetionsOfProductStockPrice(int productStockPriceId)
    {
        var response= await _mediator.Send(new GetSectionOfProductStockPriceQueryReq(productStockPriceId));
        return new JsonResult(response);
    }
    public async Task<IActionResult> OnPostDeleteSetionsOfProductStockPrice(int ProductStockPriceSectionId,int ProductStockPriceId)
    {
        await _mediator.Send(new DeleteProdcutStockPriceSectionCommandReq(ProductStockPriceSectionId));
        var response = await _mediator.Send(new GetSectionOfProductStockPriceQueryReq(ProductStockPriceId));
        return new JsonResult(response);
    }
    public async Task<IActionResult> OnPostChangeShowInDiscountPanel(int ProductStockPriceSectionId,int isShow)
    {
        var item =await _mediator.Send(new ProductStockPriceReadCommandReq(ProductStockPriceSectionId));
        switch (isShow)
        {
            case 0:
                item.ShowInDiscountPanels = false; break;
            case 1:
                item.ShowInDiscountPanels = true; break;
        }
            
        var response= await _mediator.Send(new ProductStockPriceUpdateCommandReq(item));
        return new JsonResult(response);
    }

    public async Task<IActionResult> OnPostAddProductStockPriceSection(int ProductStockPriceSectionId, int sectionId)
    {
        await _mediator.Send(new CreateProdcutStockPriceSectionCommandReq(ProductStockPriceSectionId,sectionId));
        var response = await _mediator.Send(new GetSectionOfProductStockPriceQueryReq(ProductStockPriceSectionId));
        return new JsonResult(response);
            
    }
}