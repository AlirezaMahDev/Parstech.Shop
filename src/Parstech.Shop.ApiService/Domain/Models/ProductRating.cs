namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductRating
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Rating { get; set; }

    public virtual Product Product { get; set; } = null!;
}