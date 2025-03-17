namespace Parstech.Shop.Shared.DTOs;

public class SalesPagingDto
{
    public int Take { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array List { get; set; }
    public List<OrderDetailSaleDto> sales { get; set; }
    public Array StoresSelect { get; set; }
}