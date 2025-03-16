using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.State;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Admin.Users
{
    [Authorize(Roles = "SupperUser")]
    public class StoresModel : PageModel
    {
        #region Constructor

        private readonly IStoreAdminGrpcClient _storeAdminClient;
        private readonly IUserAdminGrpcClient _userAdminClient;

        public StoresModel(
            IStoreAdminGrpcClient storeAdminClient,
            IUserAdminGrpcClient userAdminClient)
        {
            _storeAdminClient = storeAdminClient;
            _userAdminClient = userAdminClient;
        }

        #endregion

        #region Properties

        public List<UserDto> Users { get; set; }
        public List<SteteDto> stetes { get; set; }

        [BindProperty]
        public UserStoreDto Input { get; set; }

        [BindProperty]
        public RepresentationDto RepInput { get; set; }

        [BindProperty]
        public List<UserStoreDto> List { get; set; }

        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty]
        public int StoreId { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            // Get all stores
            List = await _storeAdminClient.GetUserStoresAsync();
            
            // Get user data if needed
            var userParameter = new UserParameterDto { PageId = 1, Take = 1000 }; // Get all users
            var userResponse = await _userAdminClient.GetUsersAsync(userParameter);
            if (userResponse != null)
            {
                Users = userResponse.Items;
            }
            
            return Page();
        }

        #endregion

        #region GetData

        public async Task<IActionResult> OnGetData()
        {
            List = await _storeAdminClient.GetUserStoresAsync();
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion

        #region PostData

        public async Task<IActionResult> OnPostData()
        {
            List = await _storeAdminClient.GetUserStoresAsync();
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion

        #region UpdateAndCreate

        public async Task<IActionResult> OnPostUpdateAndCreate()
        {
            if (Input.Id != 0)
            {
                // Update existing store
                var result = await _storeAdminClient.UpdateStoreAsync(Input);
                Response.IsSuccessed = result.IsSuccessed;
                Response.Message = result.IsSuccessed ? "فروشگاه با موفقیت ویرایش شد" : result.Message;
            }
            else
            {
                // Create new store
                var result = await _storeAdminClient.CreateStoreAsync(Input);
                Response.IsSuccessed = result.IsSuccessed;
                Response.Message = result.IsSuccessed ? "فروشگاه با موفقیت ایجاد شد" : result.Message;
            }
            
            return new JsonResult(Response);
        }

        #endregion

        #region Store

        public async Task<IActionResult> OnPostStore()
        {
            if (StoreId != 0)
            {
                Input = await _storeAdminClient.GetStoreByIdAsync(StoreId);
                Response.Object = Input;
                Response.IsSuccessed = true;
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "فروشگاه یافت نشد";
            }
            
            return new JsonResult(Response);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> OnPostDelete()
        {
            var result = await _storeAdminClient.DeleteStoreAsync(StoreId);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.IsSuccessed ? "فروشگاه با موفقیت حذف شد" : result.Message;
            
            return new JsonResult(Response);
        }

        #endregion
    }
}
