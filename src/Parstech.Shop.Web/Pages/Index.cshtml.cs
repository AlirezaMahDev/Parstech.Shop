using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly ProductGrpcClient _productClient;
    private readonly OrderGrpcClient _orderClient;
    private readonly SectionGrpcClient _sectionClient;
    private readonly UserProductGrpcClient _userProductClient;
    private readonly IProductRepository _productRep;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public IndexModel(
        ProductGrpcClient productClient,
        OrderGrpcClient orderClient,
        SectionGrpcClient sectionClient,
        UserProductGrpcClient userProductClient,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IProductRepository productRep)
    {
        _productClient = productClient;
        _orderClient = orderClient;
        _sectionClient = sectionClient;
        _userProductClient = userProductClient;
        _userManager = userManager;
        _signInManager = signInManager;
        _productRep = productRep;
    }

    #endregion

    #region Properties

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public List<SectionAndDetailsDto> Sections { get; set; }

    #endregion

    #region Get

    #endregion

    public async Task<IActionResult> OnGet()
    {
        try
        {
            var sectionRequest = new SectionRequest();
            var sectionResponse = await _sectionClient.GetSectionsAsync(sectionRequest);

            Sections = sectionResponse.Sections.Select(s => new SectionAndDetailsDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Image = s.Image,
                    ParentId = s.ParentId,
                    IsActive = s.IsActive,
                    Details = s.Details.Select(d => new SectionDetailDto
                        {
                            Id = d.Id,
                            SectionId = d.SectionId,
                            Title = d.Title,
                            Description = d.Description,
                            Image = d.Image,
                            Link = d.Link,
                            IsActive = d.IsActive,
                            Order = d.Order
                        })
                        .ToList()
                })
                .ToList();

            return Page();
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error loading sections: {ex.Message}";
            return Page();
        }
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostSearch(string Filter)
    {
        try
        {
            string userName = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty;
            var searchResults = await _productClient.SearchProductsAsync(Filter, 4);

            Response.IsSuccessed = true;

            var productList = searchResults.ProductList.Select(p => new
                {
                    p.Id,
                    p.Name,
                    LatinName = p.LatinName?.Value,
                    p.Price,
                    p.SalePrice,
                    p.DiscountPrice,
                    p.Image,
                    ShortDescription = p.ShortDescription?.Value,
                    VariationName = p.VariationName?.Value
                })
                .ToList();

            Response.Object = productList;

            return new(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error searching products: {ex.Message}";
            return new(Response);
        }
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostCompare(int ProductId)
    {
        if (User.Identity.IsAuthenticated)
        {
            try
            {
                var product = await _productClient.GetProductAsync(ProductId);
                var request = new CreateUserProductRequest
                {
                    UserName = User.Identity.Name, ProductId = ProductId, Type = "Compare"
                };

                var response = await _userProductClient.CreateUserProductAsync(request);

                if (response.Success)
                {
                    Response.Object = new { product.Id, product.Name, product.Price, product.Image };
                    Response.IsSuccessed = true;
                    Response.Message = response.Message;
                }
                else
                {
                    Response.IsSuccessed = false;
                    Response.Message = response.Message;
                }
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = $"Error adding product to compare: {ex.Message}";
            }
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Please login first";
        }

        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostCompareDelete(int userProductId)
    {
        try
        {
            var request = new DeleteUserProductRequest { UserProductId = userProductId };
            var response = await _userProductClient.DeleteUserProductAsync(request);

            Response.IsSuccessed = response.Success;
            Response.Message = response.Message;
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error deleting product from compare: {ex.Message}";
        }

        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostFavorite(int ProductId)
    {
        if (User.Identity.IsAuthenticated)
        {
            try
            {
                var product = await _productClient.GetProductAsync(ProductId);
                var request = new CreateUserProductRequest
                {
                    UserName = User.Identity.Name, ProductId = ProductId, Type = "Favorite"
                };

                var response = await _userProductClient.CreateUserProductAsync(request);

                Response.Object = new { product.Id, product.Name, product.Price, product.Image };
                Response.IsSuccessed = response.Success;
                Response.Message = response.Message;
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = $"Error adding product to favorites: {ex.Message}";
            }
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Please login first";
        }

        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostAddToOrder(int ProductId)
    {
        try
        {
            var product = await _productClient.GetProductAsync(ProductId);

            Response.Object = new { product.Id, product.Name, product.Price, product.Image };
            Response.IsSuccessed = true;
            Response.Message = $"Product {product.Name} added to cart";
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error adding product to cart: {ex.Message}";
        }

        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostCreateCheckout(int productId)
    {
        try
        {
            string userName = User.Identity.IsAuthenticated ? User.Identity.Name : "-";
            List<int> productIds = new() { productId };
            var result = await _orderClient.CreateOrderAsync(userName, productIds, 1, 1);

            Response.IsSuccessed = result.Status;
            Response.Message = result.Message;
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error creating checkout: {ex.Message}";
        }

        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostLogoutUser()
    {
        // This method remains the same as it doesn't interact with gRPC
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
            Response.IsSuccessed = true;
            Response.Message = "شما با موفقیت خارج شدید";
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "شما وارد نشده اید";
        }

        return new(Response);
    }
}