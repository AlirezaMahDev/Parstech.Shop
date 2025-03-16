using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Section;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Components
{
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
}
