using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Components;

public class GetCategurySelectModel : PageModel
{
    #region Constructor

    private readonly ISelectionsAdminGrpcClient _selectionsClient;

    public GetCategurySelectModel(ISelectionsAdminGrpcClient selectionsClient)
    {
        _selectionsClient = selectionsClient;
    }

    #endregion

    public List<CategurySelectDto> list { get; set; }

    public async Task OnGet()
    {
        list = await _selectionsClient.GetCategoriesSelectAsync();
    }
}