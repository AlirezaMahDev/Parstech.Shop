using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.ProductGallery
{
    public class ProductGalleryDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImageName { get; set; } = null!;
        public IFormFile File { get; set; } = null!;

        public string Alt { get; set; } = null!;

        public bool IsMain { get; set; }
    }
    public class UploadViewModel
    {
        public List<IFormFile> Files { get; set; }
    }


}
