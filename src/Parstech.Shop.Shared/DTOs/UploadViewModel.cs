using Microsoft.AspNetCore.Http;

namespace Parstech.Shop.Shared.DTOs;

public class UploadViewModel
{
    public List<IFormFile> Files { get; set; }
}