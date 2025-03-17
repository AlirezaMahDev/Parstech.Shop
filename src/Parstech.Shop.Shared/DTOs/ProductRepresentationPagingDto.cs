namespace Parstech.Shop.Shared.DTOs;

public class ProductRepresentationPagingDto
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int repId { get; set; }
    public Array List { get; set; }
}