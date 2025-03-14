using Microsoft.AspNetCore.Mvc;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.ViewComponents
{
    [ViewComponent(Name = "Categuries")]
    public class CateguriesViewComponent : ViewComponent
    {
        private readonly CategoryGrpcClient _categoryGrpcClient;
        
        public CateguriesViewComponent(CategoryGrpcClient categoryGrpcClient)
        {
            _categoryGrpcClient = categoryGrpcClient;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesMenu = await _categoryGrpcClient.GetCategoriesMenuAsync();
            return View(categoriesMenu);
        }
    }
}
