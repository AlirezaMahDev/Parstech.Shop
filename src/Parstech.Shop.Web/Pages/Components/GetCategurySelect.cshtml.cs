using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Categury;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Components
{
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
}
