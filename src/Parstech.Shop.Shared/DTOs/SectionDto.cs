namespace Parstech.Shop.Shared.DTOs;

public class SectionDto
{
    public int Id { get; set; }

    public string SectionName { get; set; } = null!;

    public int Sort { get; set; }

    public int? CateguryId { get; set; }
    public int? ProductId { get; set; }
    public int? StoreId { get; set; }

    public int SectionTypeId { get; set; }
}