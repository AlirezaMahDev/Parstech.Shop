namespace Parstech.Shop.Shared.DTOs;

public class ProductPageingDto
{
    public int CurrentPage { get; set; }

    public int PageCount { get; set; }
    public int Searched { get; set; }
    public Array ProductDtos { get; set; }
    public List<ProductDto> ProductList { get; set; }
}