namespace Parstech.Shop.Shared.DTOs;

public class ProductLogDto
{
    public int Id { get; set; }

    public int ProductLogTypeId { get; set; }
    public string ProductLogTypeName { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }
    public string UserName { get; set; }

    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;
}