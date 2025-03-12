using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.SectionDetail
{
    public class SectionDetailDto
    {
        public int Id { get; set; }

        public int SectionId { get; set; }
        
        public IFormFile? ImageFile { get; set; }
        public IFormFile? BackgroundImageFile { get; set; }


        public string? Image { get; set; }
        public string? BackgroundImage { get; set; }
        public string? BackgroundColor { get; set; }

        public string? Alt { get; set; }

        public string? Link { get; set; }

        public string? Caption { get; set; }

        public string? SubCaption { get; set; }

        public int SectionTypeId { get; set; }

        public int? CateguryId { get; set; }
        public string? SlideNavName { get; set; }
        public string? ResponsiveSize { get; set; }
        public int? ColSpace { get; set; }
    }
}
