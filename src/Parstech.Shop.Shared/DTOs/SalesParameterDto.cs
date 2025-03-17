namespace Parstech.Shop.Shared.DTOs;

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