namespace Parstech.Shop.ApiService.Application.DTOs;

public class BrandDto
{
    public int BrandId { get; set; }

    public string BrandTitle { get; set; } = null!;
    public string LatinBrandTitle { get; set; } = null!;

    public string BrandImage { get; set; } = null!;

    public bool IsDelete { get; set; }

    public string? ChangeByUserName { get; set; }

    public DateTime? LastChangeTime { get; set; }
    public IFormFile BrandFile { get; set; } = null!;
}