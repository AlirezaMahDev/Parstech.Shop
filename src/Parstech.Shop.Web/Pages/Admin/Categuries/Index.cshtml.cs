using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Categuries;

[Authorize(Roles = "SupperUser,Sale")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly CategoryAdminGrpcClient _categoryAdminClient;

    public IndexModel(CategoryAdminGrpcClient categoryAdminClient)
    {
        _categoryAdminClient = categoryAdminClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ParameterDto Parameter { get; set; } = new ParameterDto();

    //categuries
    [BindProperty]
    public PagingDto List { get; set; }

    //categury
    [BindProperty]
    public CateguryDto CateguryDto { get; set; }

    //Parent categury
    [BindProperty]
    public List<CateguryDto> Parents { get; set; }


    [BindProperty]
    public int categuryId { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public string FilterCat { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        // Use gRPC client to get category parents
        var parentsResponse = await _categoryAdminClient.GetCategoryParentsAsync();

        // Map gRPC response to application DTO
        Parents = parentsResponse.Categories.Select(c => MapToCategoryDto(c)).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.TakePage = 300;

        // Use gRPC client to get category paging data
        var categoriesResponse = await _categoryAdminClient.GetCategoriesForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(categoriesResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;

        // Use gRPC client to get category paging data
        var categoriesResponse = await _categoryAdminClient.GetCategoriesForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(categoriesResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;

        // Use gRPC client to get category paging data
        var categoriesResponse = await _categoryAdminClient.GetCategoriesForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(categoriesResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Add Or EditCategury

    public async Task<IActionResult> OnPostCategury()
    {
        // Use gRPC client to get category by ID
        var category = await _categoryAdminClient.GetCategoryAsync(categuryId);

        // Map gRPC response to application DTO
        CateguryDto = MapToCategoryDto(category);
        Response.Object = CateguryDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreate(bool IsParent, bool ShowMenu)
    {
        CateguryDto.IsParnet = IsParent;
        CateguryDto.Show = ShowMenu;

        if (CateguryDto.GroupId != 0)
        {
            CateguryDto.Image = "05.png";

            // Map application DTO to gRPC DTO
            var categoryGrpc = MapToGrpcCategoryDto(CateguryDto);

            // Use gRPC client to update category
            var response = await _categoryAdminClient.UpdateCategoryAsync(categoryGrpc);

            Response.Object = CateguryDto;
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
        else
        {
            CateguryDto.Image = "05.png";

            // Map application DTO to gRPC DTO
            var categoryGrpc = MapToGrpcCategoryDto(CateguryDto);

            // Use gRPC client to create category
            var response = await _categoryAdminClient.CreateCategoryAsync(categoryGrpc);

            Response.Object = CateguryDto;
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        // Use gRPC client to get all categories
        var categoriesResponse = await _categoryAdminClient.GetAllCategoriesAsync(FilterCat);

        // Map gRPC response to application DTO
        var categories = categoriesResponse.Categories.Select(c => MapToCategoryDto(c)).ToList();

        Response.Object = categories;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteCategury()
    {
        // Use gRPC client to delete category
        var response = await _categoryAdminClient.DeleteCategoryAsync(categuryId);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;

        if (!string.IsNullOrEmpty(response.Object))
        {
            Response.Object = response.Object;
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Mapping Methods

    private PagingDto MapToPagingDto(Shop.Shared.Protos.CategoryAdmin.CategoryPageingDto dto)
    {
        var result = new PagingDto { CurrentPage = dto.CurrentPage, PageCount = dto.PageCount };

        result.List = dto.List.Select(MapToCategoryDto).ToList();

        return result;
    }

    private CateguryDto MapToCategoryDto(Shop.Shared.Protos.CategoryAdmin.CategoryDto dto)
    {
        return new CateguryDto
        {
            GroupId = dto.GroupId,
            GroupTitle = dto.GroupTitle,
            LatinGroupTitle = dto.LatinGroupTitle,
            ParentId = dto.ParentId,
            Image = dto.Image,
            IsParnet = dto.IsParnet,
            Show = dto.Show
        };
    }

    private Parstech.Shop.Shared.Protos.CategoryAdmin.CategoryDto MapToGrpcCategoryDto(CateguryDto category)
    {
        return new Shop.Shared.Protos.CategoryAdmin.CategoryDto
        {
            GroupId = category.GroupId,
            GroupTitle = category.GroupTitle ?? string.Empty,
            LatinGroupTitle = category.LatinGroupTitle ?? string.Empty,
            ParentId = category.ParentId,
            Image = category.Image ?? string.Empty,
            IsParnet = category.IsParnet,
            Show = category.Show
        };
    }

    #endregion
}