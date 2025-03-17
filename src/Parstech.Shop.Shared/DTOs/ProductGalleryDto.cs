using Microsoft.AspNetCore.Http;

namespace Parstech.Shop.Shared.DTOs;

public class ProductGalleryDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ImageName { get; set; } = null!;
    public IFormFile File { get; set; } = null!;

    public string Alt { get; set; } = null!;

    public bool IsMain { get; set; }
}