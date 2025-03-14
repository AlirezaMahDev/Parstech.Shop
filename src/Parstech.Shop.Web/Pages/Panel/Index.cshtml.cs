using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.UserBilling;

namespace Shop.Web.Pages.Panel
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly UserGrpcClient _userClient;
        private readonly UserPreferencesGrpcClient _userPreferencesClient;
        
        public IndexModel(
            UserGrpcClient userClient,
            UserPreferencesGrpcClient userPreferencesClient)
        {
            _userClient = userClient;
            _userPreferencesClient = userPreferencesClient;
        }
        #endregion
        
        #region Properties
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public UserBillingDto UserBillingDto { get; set; }
        #endregion
        
        #region Get
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {
            var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var billingResponse = await _userPreferencesClient.GetUserBillingAsync(currentUser.Id);
            
            UserBillingDto = new UserBillingDto
            {
                Id = billingResponse.Id,
                UserId = billingResponse.UserId,
                CompanyName = billingResponse.CompanyName,
                EconomicCode = billingResponse.EconomicCode,
                NationalId = billingResponse.NationalId,
                RegistrationNumber = billingResponse.RegistrationNumber,
                PhoneNumber = billingResponse.PhoneNumber,
                PostalCode = billingResponse.PostalCode,
                Address = billingResponse.Address
            };
            
            Response.IsSuccessed = true;
            Response.Object = UserBillingDto;
            return new JsonResult(Response);
        }
        #endregion
        
        #region Create Update
        public async Task<IActionResult> OnPostUpdate()
        {
            var updatedBillingResponse = await _userPreferencesClient.UpdateUserBillingAsync(
                UserBillingDto.Id,
                UserBillingDto.UserId,
                UserBillingDto.CompanyName,
                UserBillingDto.EconomicCode,
                UserBillingDto.NationalId,
                UserBillingDto.RegistrationNumber,
                UserBillingDto.PhoneNumber,
                UserBillingDto.PostalCode,
                UserBillingDto.Address);
            
            var updatedUserBilling = new UserBillingDto
            {
                Id = updatedBillingResponse.Id,
                UserId = updatedBillingResponse.UserId,
                CompanyName = updatedBillingResponse.CompanyName,
                EconomicCode = updatedBillingResponse.EconomicCode,
                NationalId = updatedBillingResponse.NationalId,
                RegistrationNumber = updatedBillingResponse.RegistrationNumber,
                PhoneNumber = updatedBillingResponse.PhoneNumber,
                PostalCode = updatedBillingResponse.PostalCode,
                Address = updatedBillingResponse.Address
            };
            
            Response.Object = updatedUserBilling;
            Response.IsSuccessed = true;
            Response.Message = "اطلاعات حساب کاربری با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        #endregion

        #region Get Wallet Amount
        public async Task<IActionResult> OnPostAmount()
        {
            var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var amount = await _userPreferencesClient.GetWalletAmountAsync(currentUser.Id);
            
            Response.Object = amount;
            return new JsonResult(Response);
        }
        
        public async Task<IActionResult> OnPostCoin()
        {
            var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var coin = await _userPreferencesClient.GetWalletCoinAsync(currentUser.Id);
            
            Response.Object = coin;
            return new JsonResult(Response);
        }
        
        public async Task<IActionResult> OnPostFecilities()
        {
            var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var facilities = await _userPreferencesClient.GetWalletFacilitiesAsync(currentUser.Id);
            
            Response.Object = facilities;
            return new JsonResult(Response);
        }
        #endregion
    }
}
