namespace Parstech.Shop.ApiService.Application.DTOs;

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

public class LogDto
{
    public List<ProductLogDto> SaleLogDtos { get; set; }
    public List<ProductLogDto> BaseLogDtos { get; set; }
    public List<ProductLogDto> PriceLogDtos { get; set; }
    public List<ProductLogDto> DiscountLogDtos { get; set; }
    public List<ProductRepresentationDto> ProductRepresentationDtos { get; set; }
}

public class ParameterLogDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public int ProductId { get; set; }
}