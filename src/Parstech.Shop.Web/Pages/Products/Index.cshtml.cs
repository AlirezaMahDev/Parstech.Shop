using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Products;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly ProductGrpcClient _productClient;
    private readonly CategoryGrpcClient _categoryClient;
    private readonly BrandGrpcClient _brandClient;
    private readonly UserStoreGrpcClient _storeClient;

    public IndexModel(
        ProductGrpcClient productClient,
        CategoryGrpcClient categoryClient,
        BrandGrpcClient brandClient,
        UserStoreGrpcClient storeClient)
    {
        _productClient = productClient;
        _categoryClient = categoryClient;
        _brandClient = brandClient;
        _storeClient = storeClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ProductSearchParameterDto Parameter { get; set; } = new ProductSearchParameterDto();

    [BindProperty]
    public ProductPageingDto List { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public string Categury { get; set; }

    [BindProperty]
    public string FilterCat { get; set; }

    [BindProperty]
    public int Type { get; set; }

    [BindProperty]
    public string Filter { get; set; }

    public List<CateguryDto> categuries { get; set; }
    public List<BrandDto> Brands { get; set; }
    public List<UserStoreDto> Stores { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(string cat)
    {
        try
        {
            Categury = cat;

            // Get brands
            var brandsResponse = await _brandClient.GetBrandsAsync();
            Brands = brandsResponse.Brands.Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    LatinName = b.LatinName,
                    Description = b.Description,
                    Image = b.Image,
                    IsActive = b.IsActive,
                    Order = b.Order,
                    Logo = b.Logo,
                    Website = b.Website,
                    Country = b.Country
                })
                .ToList();

            // Get stores
            var storesResponse = await _storeClient.GetStoresAsync();
            Stores = storesResponse.Stores.Select(s => new UserStoreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    LatinName = s.LatinName,
                    Description = s.Description,
                    Image = s.Image,
                    IsActive = s.IsActive,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    Website = s.Website,
                    Instagram = s.Instagram,
                    Telegram = s.Telegram,
                    Whatsapp = s.Whatsapp,
                    UserName = s.UserName,
                    CreatedAt = DateTime.Parse(s.CreatedAt),
                    UpdatedAt = DateTime.Parse(s.UpdatedAt)
                })
                .ToList();

            // Get categories
            categuries = new();
            var parentCategories = await _categoryClient.GetParentCategoriesAsync();

            foreach (var parent in parentCategories.Categories)
            {
                categuries.Add(new CateguryDto
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    LatinName = parent.LatinName,
                    Description = parent.Description,
                    Image = parent.Image,
                    ParentId = parent.ParentId,
                    GroupId = parent.GroupId,
                    IsActive = parent.IsActive,
                    Order = parent.Order
                });

                var subParentCategories = await _categoryClient.GetSubCategoriesAsync(parent.GroupId);
                foreach (var subParent in subParentCategories.Categories)
                {
                    categuries.Add(new CateguryDto
                    {
                        Id = subParent.Id,
                        Name = subParent.Name,
                        LatinName = subParent.LatinName,
                        Description = subParent.Description,
                        Image = subParent.Image,
                        ParentId = subParent.ParentId,
                        GroupId = subParent.GroupId,
                        IsActive = subParent.IsActive,
                        Order = subParent.Order
                    });

                    var subCategories = await _categoryClient.GetSubCategoriesAsync(subParent.GroupId);
                    foreach (var sub in subCategories.Categories)
                    {
                        categuries.Add(new CateguryDto
                        {
                            Id = sub.Id,
                            Name = sub.Name,
                            LatinName = sub.LatinName,
                            Description = sub.Description,
                            Image = sub.Image,
                            ParentId = sub.ParentId,
                            GroupId = sub.GroupId,
                            IsActive = sub.IsActive,
                            Order = sub.Order
                        });
                    }
                }
            }

            return Page();
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error loading data: {ex.Message}";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostData(int skip, string store)
    {
        try
        {
            Parameter.Take = 30;
            string userName = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            // Get products using gRPC
            var productRequest = new ProductPagingRequest
            {
                Skip = skip,
                Take = Parameter.Take,
                Category = Parameter.Categury,
                UserName = userName,
                Store = store
            };

            var productResponse = await _productClient.GetPagedProductsAsync(productRequest);

            // Map the response
            List = new ProductPageingDto
            {
                Products = productResponse.Products.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        LatinName = p.LatinName,
                        Description = p.Description,
                        Price = p.Price,
                        SalePrice = p.SalePrice,
                        DiscountPrice = p.DiscountPrice,
                        Image = p.Image,
                        IsActive = p.IsActive,
                        CreatedAt = DateTime.Parse(p.CreatedAt),
                        UpdatedAt = DateTime.Parse(p.UpdatedAt)
                    })
                    .ToList(),
                TotalCount = productResponse.TotalCount,
                PageCount = productResponse.PageCount
            };

            // Get category by latin name
            var category = await _categoryClient.GetCategoryByLatinNameAsync(Parameter.Categury);

            Response.Object = List;
            Response.Object2 = new CateguryDto
            {
                Id = category.Id,
                Name = category.Name,
                LatinName = category.LatinName,
                Description = category.Description,
                Image = category.Image,
                ParentId = category.ParentId,
                GroupId = category.GroupId,
                IsActive = category.IsActive,
                Order = category.Order
            };
            Response.CurrentParameter = Parameter;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error loading products: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion
}