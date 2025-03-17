using Microsoft.AspNetCore.Http;

namespace Parstech.Shop.Shared.DTOs;

public class ProductRepresentationDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }
    public int QuantityPerBundle { get; set; }

    public string UniqeCode { get; set; } = null!;

    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }

    public int TypeId { get; set; }
    public string Type { get; set; }

    public int ProductStockPriceId { get; set; }
    public string ProductName { get; set; }

    public int RepresntationId { get; set; }
    public string RepresntationName { get; set; }


    public string ProductCode { get; set; }

    public int UserId { get; set; }
    public string UserName { get; set; }

    public string? FileName { get; set; }
    public IFormFile File { get; set; }
}