namespace Parstech.Shop.ApiService.Application.DTOs;

public class PayTypeDto
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }
    public bool IsActive { get; set; }
}