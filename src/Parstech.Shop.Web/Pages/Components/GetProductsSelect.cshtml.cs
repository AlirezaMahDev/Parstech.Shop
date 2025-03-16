using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Components
{
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
}
