namespace Parstech.Shop.Context.Domain.Models;

public partial class UserProduct
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string Type { get; set; } = null!;
}
