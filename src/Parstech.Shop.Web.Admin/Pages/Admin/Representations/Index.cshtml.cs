using System.Globalization;

using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductLog;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.DTOs.RepresentationType;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.RepresentationType.Requests.Commands;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;
using Parstech.Shop.Context.Application.Validators.Product;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Representations;

[Authorize(Roles = "SupperUser,Inventory,Store")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductStockPriceRepository _productStockRep;

    public IndexModel(IMediator mediator,
        IMapper mapper,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ProductRepresenationParameterDto Parameter { get; set; } = new();

    //categuries
    [BindProperty]
    public ProductRepresentationPagingDto List { get; set; }


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
        List = await _mediator.Send(new ProductRepresentaionPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            Response.role = "Store";
        }
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostItem()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;
        List = await _mediator.Send(new ProductRepresentaionPagingQueryReq(Parameter));
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
        List = await _mediator.Send(new ProductRepresentaionPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        List = await _mediator.Send(new ProductRepresentaionPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Price

    public async Task<IActionResult> OnPostPriceItem()
    {
        productStock = await _mediator.Send(new ProductStockPriceReadCommandReq(productId));

        Response.Object = productStock;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostEditPriceItem()
    {
        var currentproductStock = await _mediator.Send(new ProductStockPriceReadCommandReq(productStock.Id));
        productStock.Price = long.Parse(productStock.TextPrice.Replace(",", ""));
        productStock.SalePrice = long.Parse(productStock.TextSalePrice.Replace(",", ""));
        productStock.DiscountPrice = long.Parse(productStock.TextDiscountPrice.Replace(",", ""));
        productStock.BasePrice = long.Parse(productStock.TextBasePrice.Replace(",", ""));
        productStock.Quantity = currentproductStock.Quantity;
        productStock.MaximumSaleInOrder = currentproductStock.MaximumSaleInOrder;
        productStock.StoreId = currentproductStock.StoreId;
        productStock.RepId = currentproductStock.RepId;
        productStock.TaxId = currentproductStock.TaxId;
        productStock.QuantityPerBundle = currentproductStock.QuantityPerBundle;
        productStock.DiscountDate = currentproductStock.DiscountDate;
        #region Validator
        var validator = new ProductPriceValidator();
        var valid = validator.Validate(productStock);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;

            return new JsonResult(Response);
        }
        #endregion

        if (productStock.DiscountPrice > 0)
        {
            if (productStock.DiscountDateShamsi != null)
            {
                var Date = ConvertPersianNumbersToEnglish.ToEnglishNumber(productStock.DiscountDateShamsi);

                string[] std = Date.Split('/');
                var az = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                );
                productStock.DiscountDate = az;
            }

        }
        else
        {
            productStock.DiscountDate = null;
        }


        var current = _productStockRep.DapperGetProductStockPriceById(productStock.Id);
        var currentDto = _mapper.Map<ProductStockPriceDto>(current.Result);
        var edit = await _mediator.Send(new ProductStockPriceUpdateCommandReq(productStock));
        await _mediator.Send(new PriceConflictsCreateLogQueryReq(User.Identity.Name, currentDto, edit));
        Response.Object = edit;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    #endregion

    #region Mojudi

    public async Task<IActionResult> OnPostAddProductRepresentation()
    {
        var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        ProductRepresentationDto.UserId = user.Id;
        var res = await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRepresentationDto));
        if (res.Id == 0)
        {

            Response.Message = "موجودی انبار جهت خروج کالا کافی نیست";
            Response.IsSuccessed = false;
        }
        else
        {
            Response.Object = res;
            Response.IsSuccessed = true;
        }
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostQuickAddProductRepresentation()
    {
        var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        ProductRepresentationDto.UserId = user.Id;
        var res = await _mediator.Send(new ProductRepresesntationQuickCreateCommandReq(ProductRepresentationDto));
        Response.Object = res;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    #endregion

    #region Logs

    public async Task<IActionResult> OnPostGetLogs()
    {
        LogParameter.CurrentPage = 1;
        LogParameter.TakePage = 30;
        PrParameter.CurrentPage = 1;
        PrParameter.TakePage = 30;
        PrParameter.ProductId = productId;
        PrParameter.RepId = RepId;
        Log.LogDto = await _mediator.Send(new ProductLogReadsFromProductIdQueryReq(productId));
        Log.ProductLogPaging = await _mediator.Send(new UniqProductLogPagingQueryReq(LogParameter));
        Log.ProductRepresntationChart = await _mediator.Send(new ProductRepresentationChartQueryReq(productId));
        Log.ProductRepresntationPaging = await _mediator.Send(new ProductPresentationsWithProductPagingQueryReq(PrParameter));
        Response.Object = Log;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
    #region Delete
    public async Task<IActionResult> OnPostDelete(int rep, int id)
    {
        var result = await _mediator.Send(new ProductStockPriceDeleteQueryReq(rep, id));

        Response.IsSuccessed = result;
        return new JsonResult(Response);
    }
    #endregion
}