using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.SocialSetting
{
    public class SocialSettingDto
    {
        public int Id { get; set; } = 0!;
        public string SocialName { get; set; } = null!;

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? Site { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
