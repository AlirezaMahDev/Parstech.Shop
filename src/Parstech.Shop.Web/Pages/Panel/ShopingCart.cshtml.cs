using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Response;

namespace Shop.Web.Pages.Panel
{
    [Authorize]
    public class ShopingCartModel : PageModel
    {
        #region Constructor
        private readonly UserPreferencesGrpcClient _userPreferencesClient;
        
        public ShopingCartModel(UserPreferencesGrpcClient userPreferencesClient)
        {
            _userPreferencesClient = userPreferencesClient;
        }
        #endregion

        #region Properties
        [BindProperty]
        public OrderDetailShowDto ShoppingCart { get; set; }

        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty]
        public int UserId { get; set; }
        #endregion

        #region Get
        public IActionResult OnGet()
        {
            return Page();
        }
        
        public async Task<IActionResult> OnPostData()
        {
            var cartResponse = await _userPreferencesClient.GetShoppingCartAsync(UserId);
            
            ShoppingCart = new OrderDetailShowDto
            {
                OrderId = cartResponse.OrderId,
                UserName = cartResponse.UserName,
                Total = cartResponse.Total,
                Discount = cartResponse.Discount,
                FinalPrice = cartResponse.FinalPrice,
                OrderDetails = cartResponse.Details.Select(d => new Shop.Application.DTOs.OrderDetail.OrderDetailItem
                {
                    Id = d.Id,
                    OrderId = d.OrderId,
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    ProductImage = d.ProductImage,
                    Count = d.Count,
                    Price = d.Price,
                    Discount = d.Discount,
                    Total = d.Total
                }).ToList()
            };
            
            Response.Object = ShoppingCart;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }
        #endregion
    }
}
