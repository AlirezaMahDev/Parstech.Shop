namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductComment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}