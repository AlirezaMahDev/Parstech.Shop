using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Representations;

[Authorize(Roles = "SupperUser,Inventory,Store")]
public class QuickEditModel : PageModel
{
    #region Constractor

    private readonly IRepresentationAdminGrpcClient _representationClient;
    private readonly IUserGrpcClient _userClient;
    private readonly IProductRepresesntationRepository _productRepresesntationRep;

    public QuickEditModel(
        IRepresentationAdminGrpcClient representationClient,
        IUserGrpcClient userClient,
        IProductRepresesntationRepository productRepresesntationRep)
    {
        _representationClient = representationClient;
        _userClient = userClient;
        _productRepresesntationRep = productRepresesntationRep;
    }

    #endregion

    #region

    public ProductRepresentationPagingDto list { get; set; }

    [BindProperty]
    public ProductRepresenationParameterDto parameters { get; set; } = new ProductRepresenationParameterDto();

    public ResponseDto response { get; set; } = new ResponseDto();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetData()
    {
        parameters.TakePage = 30;
        if (parameters.CurrentPage == 0)
        {
            parameters.CurrentPage = 1;
        }

        var user = await _userClient.GetUserByUserNameAsync(User.Identity.Name);
        var userStore = await _userClient.GetUserStoreByUserIdAsync(user.Id);
        if (userStore == null)
        {
            response.Object = null;
            response.IsSuccessed = false;
            return new JsonResult(response);
        }

        var rep = await _representationClient.GetRepresentationByIdAsync(userStore.RepId);
        parameters.RepId = rep.Id;

        var pagingResult = await _representationClient.GetProductRepresentationsAsync(parameters.ProductId);
        list = new ProductRepresentationPagingDto
        {
            List = pagingResult,
            CurrentPage = parameters.CurrentPage,
            PageCount = (int)Math.Ceiling(pagingResult.Count / (double)parameters.TakePage),
            RowCount = pagingResult.Count
        };

        response.Object = list;
        response.IsSuccessed = true;
        return new JsonResult(response);
    }

    #endregion

    #region SaveChanges

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostSaveChanges([FromBody] List<QuickEditDto> list)
    {
        // Assuming there's a method in the client for quick editing
        var response = new ResponseDto();

        foreach (var item in list)
        {
            var productRep = await _representationClient.GetProductRepresentationByIdAsync(item.Id);
            if (productRep != null)
            {
                productRep.Value = item.Value;
                var updateResult = await _representationClient.UpdateProductRepresentationAsync(productRep);
                if (!updateResult.IsSuccessed)
                {
                    response.IsSuccessed = false;
                    response.Message = updateResult.Message;
                    return new JsonResult(response);
                }
            }
        }

        response.IsSuccessed = true;
        response.Message = "تغییرات با موفقیت اعمال شد";
        return new JsonResult(response);
    }

    #endregion
}