namespace Parstech.Shop.Shared.DTOs;

public class StatusDto
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusLatinName { get; set; } = null!;

    public string? Icon { get; set; }

    public int? Olaviyat { get; set; }
}