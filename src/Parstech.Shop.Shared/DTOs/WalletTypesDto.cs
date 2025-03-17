namespace Parstech.Shop.ApiService.Application.DTOs;

public class WalletTypesDto
{
    public int TypeId { get; set; }

    public string TypeTitle { get; set; } = null!;

    public string? Color { get; set; }
}