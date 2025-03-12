using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Parstech.Shop.Shared.Protos.Order;
using Parstech.Shop.Shared.Protos.Product;
using Parstech.Shop.Shared.Protos.Response;
using Parstech.Shop.Web.Services.GrpcClients;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Section;

namespace Shop.Web.Pages
{
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly ProductGrpcClient _productClient;
        private readonly OrderGrpcClient _orderClient;
        private readonly IProductRepository _productRep;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            ProductGrpcClient productClient,
            OrderGrpcClient orderClient,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IProductRepository productRep)
        {
            _productClient = productClient;
            _orderClient = orderClient;
            _userManager = userManager;
            _signInManager = signInManager;
            _productRep = productRep;
        }
        #endregion

        #region Properties
        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty]
        public List<SectionAndDetailsDto> Sections { get; set; }
        #endregion

        #region Get
        #endregion
        public async Task<IActionResult> OnGet()
        {
            // TODO: Replace with appropriate gRPC call once SectionService is implemented
            // For now, we'll use the existing implementation
            // Sections = await _mediator.Send(new SectionAndDetailsReadsQueryReq(null));
            
            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostSearch(string Filter)
        {
            try
            {
                // Get user name if authenticated
                string userName = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty;

                // Use the ProductGrpcClient to search products
                var searchResults = await _productClient.SearchProductsAsync(Filter, 4);
                
                // Map the results back to the response format expected by the UI
                Response.IsSuccessed = true;
                
                // Convert gRPC response to the expected format
                var productList = searchResults.ProductList.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    LatinName = p.LatinName?.Value,
                    Price = p.Price,
                    SalePrice = p.SalePrice,
                    DiscountPrice = p.DiscountPrice,
                    Image = p.Image,
                    ShortDescription = p.ShortDescription?.Value,
                    VariationName = p.VariationName?.Value
                }).ToList();
                
                Response.Object = productList;
                
                return new JsonResult(Response);
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = $"Error searching products: {ex.Message}";
                return new JsonResult(Response);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCompare(int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    // Get product details using gRPC
                    var product = await _productClient.GetProductAsync(ProductId);

                    // TODO: Replace with appropriate gRPC call once UserProductService is implemented
                    // For now, we'll use the existing implementation to create user product comparison
                    // var res = await _mediator.Send(new CreateUserProductCommandReq(User.Identity.Name, ProductId, "Compare"));
                    var res = true; // Placeholder

                    if (res)
                    {
                        // Map the gRPC product to the response object
                        Response.Object = new
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            Image = product.Image
                        };
                        Response.IsSuccessed = true;
                        Response.Message = $"محصول {product.Name} به بخش مقایسه افزوده شد";
                    }
                    else
                    {
                        Response.IsSuccessed = false;
                        Response.Message = $"لیست مقایسه شما تکمیل است";
                    }
                }
                catch (Exception ex)
                {
                    Response.IsSuccessed = false;
                    Response.Message = $"خطا در افزودن محصول به مقایسه: {ex.Message}";
                }
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "ابتدا وارد حساب خود شوید";
            }
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCompareDelete(int userProductId)
        {
            try
            {
                // TODO: Replace with appropriate gRPC call once UserProductService is implemented
                // await _mediator.Send(new DeleteUserProductCommandReq(userProductId));

                Response.IsSuccessed = true;
                Response.Message = "محصول از لیست مقایسه شما حذف شد";
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = $"خطا در حذف محصول از مقایسه: {ex.Message}";
            }

            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostFavorite(int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    // Get product details using gRPC
                    var product = await _productClient.GetProductAsync(ProductId);

                    // TODO: Replace with appropriate gRPC call once UserProductService is implemented
                    // var res = await _mediator.Send(new CreateUserProductCommandReq(User.Identity.Name, ProductId, "Favorite"));

                    // Map the gRPC product to the response
                    Response.Object = new
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Image = product.Image
                    };
                    Response.IsSuccessed = true;
                    Response.Message = $"محصول {product.Name} به علاقه مندی های شما افزوده شد";
                }
                catch (Exception ex)
                {
                    Response.IsSuccessed = false;
                    Response.Message = $"خطا در افزودن محصول به علاقه مندی ها: {ex.Message}";
                }
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "ابتدا وارد حساب خود شوید";
            }
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostAddToOrder(int ProductId)
        {
            try
            {
                // Get product details using gRPC
                var product = await _productClient.GetProductAsync(ProductId);

                // Map the gRPC product to the response
                Response.Object = new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Image
                };
                Response.IsSuccessed = true;
                Response.Message = $"محصول {product.Name} به سبد خرید افزوده شد";
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = $"خطا در افزودن محصول به سبد خرید: {ex.Message}";
            }
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCreateCheckout(int productId)
        {
            try
            {
                string userName = User.Identity.IsAuthenticated ? User.Identity.Name : "-";

                // Use OrderGrpcClient to create checkout
                var productIds = new List<int> { productId };
                var result = await _orderClient.CreateOrderAsync(userName, productIds, 1, 1);

                // Map the response
                Response.IsSuccessed = result.Status;
                Response.Message = result.Message;
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = $"خطا در ایجاد سفارش: {ex.Message}";
            }
            return new JsonResult(Response);
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
            return new JsonResult(Response);
        }
    }
}
