namespace Parstech.Shop.Shared.DTOs;

public class PropertyDto
{
    public int Id { get; set; }

    public string Caption { get; set; } = null!;

    public int PropertyCateguryId { get; set; }
    public string PropertyCateguryTitle { get; set; }

    public int CateguryId { get; set; }
    public string CateguryTitle { get; set; }
}