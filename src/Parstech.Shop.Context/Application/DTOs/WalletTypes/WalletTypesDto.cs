namespace Parstech.Shop.Context.Application.DTOs.WalletTypes;

public class WalletTypesDto
{
    public int TypeId { get; set; }

    public string TypeTitle { get; set; } = null!;

    public string? Color { get; set; }
}