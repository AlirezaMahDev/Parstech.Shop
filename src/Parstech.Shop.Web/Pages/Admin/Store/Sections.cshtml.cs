using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SectionDetail;

namespace Shop.Web.Pages.Admin.Store
{
    [Authorize(Roles = "SupperUser,Store,StoreBySend")]
    public class SectionsModel : PageModel
    {
        #region Constractor

        private readonly SectionAdminGrpcClient _sectionAdminClient;
        private readonly UserGrpcClient _userClient;

        public SectionsModel(SectionAdminGrpcClient sectionAdminClient, UserGrpcClient userClient)
        {
            _sectionAdminClient = sectionAdminClient;
            _userClient = userClient;
        }

        #endregion

        #region Properties

        [BindProperty]
        public List<SectionAndDetailsDto> List { get; set; }

        [BindProperty]
        public SectionDto Section { get; set; }

        [BindProperty]
        public int SectionId { get; set; }


        [BindProperty]
        public SectionDetailDto SectionDetail { get; set; }

        [BindProperty]
        public int SectionDeatilId { get; set; }

        [BindProperty]
        public int? StoreId { get; set; }


        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();
        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            var user = await _userClient.GetCurrentUserAsync(User.Identity.Name);
            var userStores = await _userClient.GetUserStoresAsync(user.Id);
            StoreId = null;
            if (userStores != null && userStores.Any())
            {
                StoreId = userStores.First().Id;
            }
            List = await _sectionAdminClient.GetSectionsAndDetailsAsync(StoreId);
            return Page();
        }

        public async Task<IActionResult> OnPostSection()
        {
            Section = await _sectionAdminClient.GetSectionByIdAsync(SectionId);
            Response.Object = Section;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostSectionDetail()
        {
            SectionDetail = await _sectionAdminClient.GetSectionDetailByIdAsync(SectionDeatilId);
            Response.Object = SectionDetail;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion

        #region Create and Update

        public async Task<IActionResult> OnPostCreateUpdateSection()
        {
            if (SectionId != 0)
            {
                Section.Id = SectionId;
                var result = await _sectionAdminClient.UpdateSectionAsync(Section);
                Response = result;
                return new JsonResult(Response);
            }
            else
            {
                var result = await _sectionAdminClient.CreateSectionAsync(Section);
                Response = result;
                return new JsonResult(Response);
            }
        }

        public async Task<IActionResult> OnPostCreateUpdateSectionDetail()
        {
            if (SectionDeatilId != 0)
            {
                SectionDetail.Id = SectionDeatilId;
                var result = await _sectionAdminClient.UpdateSectionDetailAsync(SectionDetail);
                Response = result;
                return new JsonResult(Response);
            }
            else
            {
                SectionDetail.SectionId = SectionId;
                var result = await _sectionAdminClient.CreateSectionDetailAsync(SectionDetail);
                Response = result;
                return new JsonResult(Response);
            }
        }

        #endregion


        #region Delete

        public async Task<IActionResult> OnPostDeleteSection()
        {
            var canDelete = await _sectionAdminClient.CheckSectionCanBeDeletedAsync(SectionId);
            if (canDelete)
            {
                var result = await _sectionAdminClient.DeleteSectionAsync(SectionId);
                Response = result;
                return new JsonResult(Response);
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "تا زمانی که قسمت اصلی سایت دارای زیر مجموعه باشد امکان حذف آن وجود ندارد";
                return new JsonResult(Response);
            }
        }

        public async Task<IActionResult> OnPostDeleteSectionDetail()
        {
            var result = await _sectionAdminClient.DeleteSectionDetailAsync(SectionDeatilId);
            Response = result;
            return new JsonResult(Response);
        }

        #endregion
    }
}
