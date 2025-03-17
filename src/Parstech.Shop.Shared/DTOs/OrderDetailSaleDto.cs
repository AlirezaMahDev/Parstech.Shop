namespace Parstech.Shop.ApiService.Application.DTOs;

public class OrderDetailSaleDto
{
    public int Count { get; set; }
    public long DetailSum { get; set; }
    public long Discount { get; set; }
    public long Price { get; set; }
    public long Tax { get; set; }
    public long Total { get; set; }
    public int OrderId { get; set; }
    public string OrderCode { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }
    public int ProductStockPriceId { get; set; }
    public string Name { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
}

public class SalesParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public int StoreId { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string OrderCode { get; set; }
}

public class SalesPagingDto
{
    public int Take { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array List { get; set; }
    public List<OrderDetailSaleDto> sales { get; set; }
    public Array StoresSelect { get; set; }
}