namespace Parstech.Shop.ApiService.Domain.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandTitle { get; set; } = null!;

    public string? LatinBrandTitle { get; set; }

    public string BrandImage { get; set; } = null!;

    public bool IsDelete { get; set; }

    public string? ChangeByUserName { get; set; }

    public DateTime? LastChangeTime { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}