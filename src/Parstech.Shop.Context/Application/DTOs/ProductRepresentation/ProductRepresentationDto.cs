using Microsoft.AspNetCore.Http;

namespace Parstech.Shop.Context.Application.DTOs.ProductRepresentation;

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

public class ProductRepresentationList
{
    public int RepId { get; set; }
    public List<ProductRepresentationDto> ProductRepresentationDtos { get; set; }
}
public class ProductRepresentationPagingDto
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int repId { get; set; }
    public Array List { get; set; }
}
public class ProductRepresenationParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public int RepId { get; set; }
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Type { get; set; }
    public string Exist { get; set; }
    public int Categury { get; set; }

}

public class ProductRepresenationChartDto
{
    public string Name { get; set; }
    public int Count { get; set; }
    public string Color { get; set; }
}