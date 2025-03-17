using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser")]
public class SectionsModel : PageModel
{
    #region Constructor

    private readonly ISettingsAdminGrpcClient _settingsAdminClient;

    public SectionsModel(ISettingsAdminGrpcClient settingsAdminClient)
    {
        _settingsAdminClient = settingsAdminClient;
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
    public ResponseDto Response { get; set; } = new ResponseDto();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        // Get all sections using the gRPC client
        var parameter = new ParameterDto { PageId = 1, Take = 50 }; // Get all sections
        var sectionsResponse = await _settingsAdminClient.GetSectionsAsync(parameter);

        // Convert to the format expected by the page
        List = sectionsResponse.Data.Select(s => new SectionAndDetailsDto
            {
                Id = s.Id,
                Title = s.Title,
                Order = s.Order,
                SectionDetails = s.SectionDetails ?? new List<SectionDetailDto>()
            })
            .ToList();

        return Page();
    }

    #endregion

    #region Section

    public async Task<IActionResult> OnPostSection()
    {
        // Get section by ID using the gRPC client
        var parameter = new ParameterDto { PageId = 1, Take = 50 };
        var sectionsResponse = await _settingsAdminClient.GetSectionsAsync(parameter);

        Section = sectionsResponse.Data.FirstOrDefault(s => s.Id == SectionId);

        Response.Object = Section;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region SectionDetails

    public async Task<IActionResult> OnPostSectionDetail()
    {
        // Get section by ID using the gRPC client
        var parameter = new ParameterDto { PageId = 1, Take = 50 };
        var sectionsResponse = await _settingsAdminClient.GetSectionsAsync(parameter);

        var section = sectionsResponse.Data.FirstOrDefault(s => s.Id == SectionId);
        SectionDetail = section?.SectionDetails?.FirstOrDefault(sd => sd.Id == SectionDeatilId);

        Response.Object = SectionDetail;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Create Or Update Section

    public async Task<IActionResult> OnPostCreateUpdateSection()
    {
        ResponseDto result;

        if (Section.Id != 0)
        {
            // Update section
            result = await _settingsAdminClient.UpdateSectionAsync(Section);
            Response.Message = "بخش با موفقیت ویرایش شد";
        }
        else
        {
            // Create section
            result = await _settingsAdminClient.CreateSectionAsync(Section);
            Response.Message = "بخش با موفقیت افزوده شد";
        }

        Response.IsSuccessed = result.IsSuccessed;
        if (!result.IsSuccessed)
        {
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Create Or Update SectionDetail

    public async Task<IActionResult> OnPostCreateUpdateSectionDetail()
    {
        // Since the gRPC client doesn't have methods for section details directly,
        // we need to get the section first, update its details, and save the whole section
        var parameter = new ParameterDto { PageId = 1, Take = 50 };
        var sectionsResponse = await _settingsAdminClient.GetSectionsAsync(parameter);

        var section = sectionsResponse.Data.FirstOrDefault(s => s.Id == SectionDetail.SectionId);
        if (section == null)
        {
            Response.IsSuccessed = false;
            Response.Message = "بخش مورد نظر یافت نشد";
            return new JsonResult(Response);
        }

        if (section.SectionDetails == null)
        {
            section.SectionDetails = new List<SectionDetailDto>();
        }

        if (SectionDetail.Id != 0)
        {
            // Update existing detail
            var existingDetail = section.SectionDetails.FirstOrDefault(sd => sd.Id == SectionDetail.Id);
            if (existingDetail != null)
            {
                existingDetail.Title = SectionDetail.Title;
                existingDetail.Order = SectionDetail.Order;
                existingDetail.Link = SectionDetail.Link;
                existingDetail.Description = SectionDetail.Description;
                existingDetail.Image = SectionDetail.Image;
            }

            var result = await _settingsAdminClient.UpdateSectionAsync(section);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.IsSuccessed ? "جزئیات بخش با موفقیت ویرایش شد" : result.Message;
        }
        else
        {
            // Add new detail
            // Note: In a real scenario, you would need to generate a new ID for the detail
            // or the backend should handle this
            SectionDetail.Id = section.SectionDetails.Any() ? section.SectionDetails.Max(sd => sd.Id) + 1 : 1;

            section.SectionDetails.Add(SectionDetail);

            var result = await _settingsAdminClient.UpdateSectionAsync(section);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.IsSuccessed ? "جزئیات بخش با موفقیت افزوده شد" : result.Message;
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Delete Section

    public async Task<IActionResult> OnPostDeleteSection()
    {
        var result = await _settingsAdminClient.DeleteSectionAsync(SectionId);

        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.IsSuccessed ? "بخش با موفقیت حذف شد" : result.Message;

        return new JsonResult(Response);
    }

    #endregion

    #region Delete SectionDetail

    public async Task<IActionResult> OnPostDeleteSectionDetail()
    {
        // Since the gRPC client doesn't have methods for section details directly,
        // we need to get the section first, remove the detail, and save the whole section
        var parameter = new ParameterDto { PageId = 1, Take = 50 };
        var sectionsResponse = await _settingsAdminClient.GetSectionsAsync(parameter);

        var section = sectionsResponse.Data.FirstOrDefault(s => s.Id == SectionId);
        if (section == null || section.SectionDetails == null)
        {
            Response.IsSuccessed = false;
            Response.Message = "بخش مورد نظر یافت نشد";
            return new JsonResult(Response);
        }

        var detailToRemove = section.SectionDetails.FirstOrDefault(sd => sd.Id == SectionDeatilId);
        if (detailToRemove != null)
        {
            section.SectionDetails.Remove(detailToRemove);

            var result = await _settingsAdminClient.UpdateSectionAsync(section);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.IsSuccessed ? "جزئیات بخش با موفقیت حذف شد" : result.Message;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "جزئیات بخش مورد نظر یافت نشد";
        }

        return new JsonResult(Response);
    }

    #endregion
}