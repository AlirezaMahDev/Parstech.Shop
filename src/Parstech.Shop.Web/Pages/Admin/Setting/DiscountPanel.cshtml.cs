using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser,Inventory,Store")]
public class DiscountPanelModel : PageModel
{
    #region Constructor

    private readonly IRepresentationAdminGrpcClient _representationClient;
    private readonly IUserGrpcClient _userClient;
    private readonly IProductAdminGrpcClient _productAdminClient;
    private readonly IProductDetailAdminGrpcClient _productDetailClient;

    public DiscountPanelModel(
        IRepresentationAdminGrpcClient representationClient,
        IUserGrpcClient userClient,
        IProductAdminGrpcClient productAdminClient,
        IProductDetailAdminGrpcClient productDetailClient)
    {
        _representationClient = representationClient;
        _userClient = userClient;
        _productAdminClient = productAdminClient;
        _productDetailClient = productDetailClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ProductDiscountParameterDto Parameter { get; set; } = new ProductDiscountParameterDto();

    //categuries
    [BindProperty]
    public ProductDiscountPagingDto List { get; set; }


    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

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
    public ParameterLogDto LogParameter { get; set; } = new ParameterLogDto();

    [BindProperty]
    public ProductRepresenationParameterDto PrParameter { get; set; } = new ProductRepresenationParameterDto();

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
        List = await _productAdminClient.GetDiscountProductsAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    public async Task<IActionResult> OnPostGetSetionsOfProductStockPrice(int productStockPriceId)
    {
        var sections = await _productDetailClient.GetSectionsOfProductStockPriceAsync(productStockPriceId);
        return new JsonResult(sections);
    }

    public async Task<IActionResult> OnPostDeleteSetionsOfProductStockPrice(int ProductStockPriceSectionId,
        int ProductStockPriceId)
    {
        await _productDetailClient.DeleteProductStockPriceSectionAsync(ProductStockPriceSectionId);
        var sections = await _productDetailClient.GetSectionsOfProductStockPriceAsync(ProductStockPriceId);
        return new JsonResult(sections);
    }

    public async Task<IActionResult> OnPostChangeShowInDiscountPanel(int ProductStockPriceSectionId, int isShow)
    {
        var stockPrice = await _representationClient.GetProductStockPriceAsync(ProductStockPriceSectionId);
        if (stockPrice != null)
        {
            stockPrice.ShowInDiscountPanels = isShow == 1;
            var response = await _productDetailClient.UpdateProductStockPriceAsync(stockPrice);
            return new JsonResult(response);
        }

        return new JsonResult(new ResponseDto { IsSuccessed = false, Message = "محصول یافت نشد" });
    }

    public async Task<IActionResult> OnPostAddProductStockPriceSection(int ProductStockPriceSectionId, int sectionId)
    {
        var result = await _productDetailClient.AddProductStockPriceSectionAsync(ProductStockPriceSectionId, sectionId);
        var sections = await _productDetailClient.GetSectionsOfProductStockPriceAsync(ProductStockPriceSectionId);
        return new JsonResult(sections);
    }
}