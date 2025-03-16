using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Social;
using Shop.Application.DTOs.SocialSetting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Admin.Setting
{
    [Authorize(Roles = "SupperUser")]
    public class SocialModel : PageModel
    {
        private readonly ISettingsAdminGrpcClient _settingsAdminClient;

        public SocialModel(ISettingsAdminGrpcClient settingsAdminClient)
        {
            _settingsAdminClient = settingsAdminClient;
        }
        
        [BindProperty]
        public List<SocialSettingDto> List { get; set; }

        [BindProperty]
        public SocialSettingDto Input { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty] 
        public ResponseDto Response { get; set; } = new ResponseDto();

        public async Task<IActionResult> OnGet()
        {
            // Get all social settings
            var socialDto = await _settingsAdminClient.GetSocialLinksAsync(1); // Default ID for social settings
            
            // Convert to list format for the page
            List = new List<SocialSettingDto>();
            if (socialDto != null)
            {
                // Map the properties from SocialDto to a list of SocialSettingDto
                if (!string.IsNullOrEmpty(socialDto.Facebook))
                {
                    List.Add(new SocialSettingDto { Id = 1, Title = "Facebook", Url = socialDto.Facebook });
                }
                if (!string.IsNullOrEmpty(socialDto.Twitter))
                {
                    List.Add(new SocialSettingDto { Id = 2, Title = "Twitter", Url = socialDto.Twitter });
                }
                if (!string.IsNullOrEmpty(socialDto.Instagram))
                {
                    List.Add(new SocialSettingDto { Id = 3, Title = "Instagram", Url = socialDto.Instagram });
                }
                if (!string.IsNullOrEmpty(socialDto.Linkedin))
                {
                    List.Add(new SocialSettingDto { Id = 4, Title = "LinkedIn", Url = socialDto.Linkedin });
                }
                if (!string.IsNullOrEmpty(socialDto.Youtube))
                {
                    List.Add(new SocialSettingDto { Id = 5, Title = "YouTube", Url = socialDto.Youtube });
                }
                if (!string.IsNullOrEmpty(socialDto.Telegram))
                {
                    List.Add(new SocialSettingDto { Id = 6, Title = "Telegram", Url = socialDto.Telegram });
                }
            }
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostGetOne()
        {
            var socialDto = await _settingsAdminClient.GetSocialLinksAsync(1);
            
            // Find the requested social by ID
            if (socialDto != null)
            {
                switch (Id)
                {
                    case 1:
                        Input = new SocialSettingDto { Id = 1, Title = "Facebook", Url = socialDto.Facebook };
                        break;
                    case 2:
                        Input = new SocialSettingDto { Id = 2, Title = "Twitter", Url = socialDto.Twitter };
                        break;
                    case 3:
                        Input = new SocialSettingDto { Id = 3, Title = "Instagram", Url = socialDto.Instagram };
                        break;
                    case 4:
                        Input = new SocialSettingDto { Id = 4, Title = "LinkedIn", Url = socialDto.Linkedin };
                        break;
                    case 5:
                        Input = new SocialSettingDto { Id = 5, Title = "YouTube", Url = socialDto.Youtube };
                        break;
                    case 6:
                        Input = new SocialSettingDto { Id = 6, Title = "Telegram", Url = socialDto.Telegram };
                        break;
                }
            }
            
            Response.Object = Input;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }
        
        public async Task<IActionResult> OnPost()
        {
            // Get current settings
            var socialDto = await _settingsAdminClient.GetSocialLinksAsync(1);
            
            // Update the specific social link based on Input.Id
            switch (Input.Id)
            {
                case 1:
                    socialDto.Facebook = Input.Url;
                    break;
                case 2:
                    socialDto.Twitter = Input.Url;
                    break;
                case 3:
                    socialDto.Instagram = Input.Url;
                    break;
                case 4:
                    socialDto.Linkedin = Input.Url;
                    break;
                case 5:
                    socialDto.Youtube = Input.Url;
                    break;
                case 6:
                    socialDto.Telegram = Input.Url;
                    break;
            }
            
            // Save the updated social settings
            var result = await _settingsAdminClient.UpdateSocialLinksAsync(1, socialDto);
            
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
            Response.Object = Input;
            
            return new JsonResult(Response);
        }
    }
}
