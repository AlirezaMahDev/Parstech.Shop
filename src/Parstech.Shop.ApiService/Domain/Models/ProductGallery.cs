namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductGallery
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ImageName { get; set; } = null!;

    public string Alt { get; set; } = null!;

    public bool IsMain { get; set; }

    public virtual Product Product { get; set; } = null!;
}