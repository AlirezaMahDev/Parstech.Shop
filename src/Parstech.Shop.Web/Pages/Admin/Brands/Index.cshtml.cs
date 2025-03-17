using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Brands;

[Authorize(Roles = "SupperUser,Sale")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly BrandAdminGrpcClient _brandAdminClient;

    public IndexModel(BrandAdminGrpcClient brandAdminClient)
    {
        _brandAdminClient = brandAdminClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ParameterDto Parameter { get; set; } = new();

    //categuries
    [BindProperty]
    public PagingDto List { get; set; }

    //brand
    [BindProperty]
    public BrandDto BrandDto { get; set; }

    [BindProperty]
    public int BrandId { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.TakePage = 30;

        // Use gRPC client to get brand paging data
        var brandsResponse = await _brandAdminClient.GetBrandsForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(brandsResponse);
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

        // Use gRPC client to get brand paging data
        var brandsResponse = await _brandAdminClient.GetBrandsForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(brandsResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;

        // Use gRPC client to get brand paging data
        var brandsResponse = await _brandAdminClient.GetBrandsForAdminAsync(
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Filter);

        // Map gRPC response to application DTO
        List = MapToPagingDto(brandsResponse);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Add Or EditCategury

    public async Task<IActionResult> OnPostItem()
    {
        // Use gRPC client to get brand by ID
        var brand = await _brandAdminClient.GetBrandAsync(BrandId);

        // Map gRPC response to application DTO
        BrandDto = MapToBrandDto(brand);
        Response.Object = BrandDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreate()
    {
        if (BrandDto.BrandId != 0)
        {
            BrandDto.ChangeByUserName = User.Identity.Name;
            BrandDto.LastChangeTime = DateTime.Now;
            BrandDto.IsDelete = false;

            // Map application DTO to gRPC DTO
            var brandGrpc = MapToGrpcBrandDto(BrandDto);

            // Use gRPC client to update brand
            var response = await _brandAdminClient.UpdateBrandAsync(brandGrpc);

            Response.Object = BrandDto;
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
        else
        {
            BrandDto.ChangeByUserName = User.Identity.Name;
            BrandDto.LastChangeTime = DateTime.Now;
            BrandDto.IsDelete = false;

            // Map application DTO to gRPC DTO
            var brandGrpc = MapToGrpcBrandDto(BrandDto);

            // Use gRPC client to create brand
            var response = await _brandAdminClient.CreateBrandAsync(brandGrpc);

            Response.Object = BrandDto;
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Mapping Methods

    private PagingDto MapToPagingDto(Shop.Shared.Protos.BrandAdmin.BrandPageingDto dto)
    {
        var result = new PagingDto { CurrentPage = dto.CurrentPage, PageCount = dto.PageCount };

        result.List = dto.List.Select(MapToBrandDto).ToList();

        return result;
    }

    private BrandDto MapToBrandDto(Shop.Shared.Protos.Product.BrandDto dto)
    {
        return new()
        {
            BrandId = dto.BrandId,
            BrandTitle = dto.BrandTitle,
            LatinBrandTitle = dto.LatinBrandTitle,
            BrandImage = dto.BrandImage,
            ChangeByUserName = dto.ChangeByUserName,
            LastChangeTime =
                !string.IsNullOrEmpty(dto.LastChangeTime) ? DateTime.Parse(dto.LastChangeTime) : DateTime.Now,
            IsDelete = dto.IsDelete
        };
    }

    private Shop.Shared.Protos.Product.BrandDto MapToGrpcBrandDto(BrandDto brand)
    {
        return new Shop.Shared.Protos.Product.BrandDto
        {
            BrandId = brand.BrandId,
            BrandTitle = brand.BrandTitle ?? string.Empty,
            LatinBrandTitle = brand.LatinBrandTitle ?? string.Empty,
            BrandImage = brand.BrandImage ?? string.Empty,
            ChangeByUserName = brand.ChangeByUserName ?? string.Empty,
            LastChangeTime = brand.LastChangeTime.ToString(),
            IsDelete = brand.IsDelete
        };
    }

    #endregion
}