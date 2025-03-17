using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Globalization;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Representations;

[Authorize(Roles = "SupperUser,Inventory,Store")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly IRepresentationAdminGrpcClient _representationClient;
    private readonly IUserGrpcClient _userClient;
    private readonly IMapper _mapper;
    private readonly IProductStockPriceRepository _productStockRep;

    public IndexModel(
        IRepresentationAdminGrpcClient representationClient,
        IUserGrpcClient userClient,
        IMapper mapper,
        IProductStockPriceRepository productStockRep)
    {
        _representationClient = representationClient;
        _userClient = userClient;
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
            Representations =
                await _representationClient.GetRepresentationsAsync(
                    new RepresentationParameterDto { CurrentPage = 1, TakePage = 100 });
        }
        else if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var user = await _userClient.GetUserByUserNameAsync(User.Identity.Name);
            var userStore = await _userClient.GetUserStoreByUserIdAsync(user.Id);
            var rep = await _representationClient.GetRepresentationByIdAsync(userStore.RepId);
            Representations.Add(rep);
        }
        else
        {
            Representations =
                await _representationClient.GetRepresentationsAsync(
                    new RepresentationParameterDto { CurrentPage = 1, TakePage = 100 });
        }

        repTypes = await _representationClient.GetRepresentationTypesAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.TakePage = 30;
        var pagingResult = await _representationClient.GetProductRepresentationsAsync(Parameter.ProductId);
        List = new()
        {
            List = pagingResult,
            CurrentPage = Parameter.CurrentPage,
            PageCount = (int)Math.Ceiling(pagingResult.Count / (double)Parameter.TakePage),
            RowCount = pagingResult.Count
        };
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
        var pagingResult = await _representationClient.GetProductRepresentationsAsync(Parameter.ProductId);
        List = new()
        {
            List = pagingResult,
            CurrentPage = Parameter.CurrentPage,
            PageCount = (int)Math.Ceiling(pagingResult.Count / (double)Parameter.TakePage),
            RowCount = pagingResult.Count
        };
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
        var pagingResult = await _representationClient.GetProductRepresentationsAsync(Parameter.ProductId);
        List = new()
        {
            List = pagingResult,
            CurrentPage = Parameter.CurrentPage,
            PageCount = (int)Math.Ceiling(pagingResult.Count / (double)Parameter.TakePage),
            RowCount = pagingResult.Count
        };
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        var pagingResult = await _representationClient.GetProductRepresentationsAsync(Parameter.ProductId);
        List = new()
        {
            List = pagingResult,
            CurrentPage = Parameter.CurrentPage,
            PageCount = (int)Math.Ceiling(pagingResult.Count / (double)Parameter.TakePage),
            RowCount = pagingResult.Count
        };
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Price

    public async Task<IActionResult> OnPostPriceItem()
    {
        productStock = await _representationClient.GetProductStockPriceAsync(productId);
        Response.Object = productStock;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditPriceItem()
    {
        var currentproductStock = await _representationClient.GetProductStockPriceAsync(productStock.Id);
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
                DateTime az = new(int.Parse(std[0]),
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

        var response = await _representationClient.UpdateProductStockPriceAsync(productStock);
        return new JsonResult(response);
    }

    #endregion

    #region Log

    public async Task<IActionResult> OnPostLog()
    {
        LogParameter.CurrentPage = 1;
        LogParameter.TakePage = 30;
        LogParameter.ProductId = productId;

        ProductLogPaging = await _representationClient.GetProductLogsAsync(LogParameter);

        Response.IsSuccessed = true;
        Response.Object = ProductLogPaging;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostLogPaging()
    {
        LogParameter.TakePage = 30;
        LogParameter.ProductId = productId;

        ProductLogPaging = await _representationClient.GetProductLogsAsync(LogParameter);

        Response.IsSuccessed = true;
        Response.Object = ProductLogPaging;
        return new JsonResult(Response);
    }

    #endregion

    #region Rep

    public async Task<IActionResult> OnPostProductRep()
    {
        var representations = await _representationClient.GetProductRepresentationsAsync(productId);
        productRepresentation = representations.FirstOrDefault();

        Response.Object = productRepresentation;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddProductRep()
    {
        ProductRepresentationDto.UserId = User.Identity.Name;

        Response = await _representationClient.AddProductRepresentationAsync(ProductRepresentationDto);
        return new JsonResult(Response);
    }

    #endregion

    #region QuickAdd

    public async Task<IActionResult> OnPostQuickAdd()
    {
        ProductRepresentationDto.UserId = User.Identity.Name;
        var representations =
            await _representationClient.GetProductRepresentationsAsync(ProductRepresentationDto.ProductId);

        if (representations.Any(x => x.RepresentationTypeId == ProductRepresentationDto.RepresentationTypeId))
        {
            Response.IsSuccessed = false;
            Response.Message = "این مشخصه برای محصول وجود دارد";
            return new JsonResult(Response);
        }

        Response = await _representationClient.QuickAddProductRepresentationAsync(ProductRepresentationDto);
        return new JsonResult(Response);
    }

    #endregion

    #region Delete

    public async Task<IActionResult> OnPostDeleteRep()
    {
        Response = await _representationClient.DeleteProductRepresentationAsync(productId);
        return new JsonResult(Response);
    }

    #endregion
}