namespace Parstech.Shop.ApiService.Application.DTOs;

public class ProductRelatedDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int FkProductId { get; set; }
}