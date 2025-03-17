using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Components;

public class GetProductsSelectModel : PageModel
{
    #region Constructor

    private readonly ISelectionsAdminGrpcClient _selectionsClient;

    public GetProductsSelectModel(ISelectionsAdminGrpcClient selectionsClient)
    {
        _selectionsClient = selectionsClient;
    }

    #endregion

    public List<ProductSelectDto> list { get; set; }

    public async Task OnGet()
    {
        list = await _selectionsClient.GetProductsSelectAsync();
    }
}