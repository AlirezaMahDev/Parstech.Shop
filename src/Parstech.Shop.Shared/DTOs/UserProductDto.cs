namespace Parstech.Shop.ApiService.Application.DTOs;

public class UserProductDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string Type { get; set; } = null!;
}