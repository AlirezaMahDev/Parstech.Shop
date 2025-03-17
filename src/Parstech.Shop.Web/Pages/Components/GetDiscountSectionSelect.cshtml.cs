using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Components;

public class GetDiscountSectionSelect : PageModel
{
    #region Constructor

    private readonly ISelectionsAdminGrpcClient _selectionsClient;

    public GetDiscountSectionSelect(ISelectionsAdminGrpcClient selectionsClient)
    {
        _selectionsClient = selectionsClient;
    }

    #endregion

    public List<SectionDto> list { get; set; }

    public async Task OnGet()
    {
        list = await _selectionsClient.GetDiscountSectionsSelectAsync();
    }
}