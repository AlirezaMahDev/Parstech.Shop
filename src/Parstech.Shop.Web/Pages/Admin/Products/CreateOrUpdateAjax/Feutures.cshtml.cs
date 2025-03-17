using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Products.CreateOrUpdateAjax;

public class FeuturesModel : PageModel
{
    #region Constructor

    private readonly IProductComponentsAdminGrpcClient _productComponentsClient;
    private readonly IProductStockPriceRepository _productStockRep;

    public FeuturesModel(
        IProductComponentsAdminGrpcClient productComponentsClient,
        IProductStockPriceRepository productStockRep)
    {
        _productComponentsClient = productComponentsClient;
        _productStockRep = productStockRep;
    }

    #endregion

    //id
    [BindProperty]
    public int? productId { get; set; }

    [BindProperty]
    public List<ProductPropertyDto> PropertyDtos { get; set; } = new();

    public async Task OnGet(int? id)
    {
        productId = id;
        if (productId != null)
        {
            var response = await _productComponentsClient.GetPropertiesOfProductAsync(productId.Value);
            if (response.IsSuccess)
            {
                PropertyDtos = response.Properties.ToList();
            }
        }
    }
}