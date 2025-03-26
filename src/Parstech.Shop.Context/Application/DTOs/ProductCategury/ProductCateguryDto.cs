namespace Parstech.Shop.Context.Application.DTOs.ProductCategury;

public class ProductCateguryDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CateguryId { get; set; }

    public string GroupTitle { get; set; } = null!;

    public int? ParentId { get; set; }

    public bool IsParnet { get; set; }
}