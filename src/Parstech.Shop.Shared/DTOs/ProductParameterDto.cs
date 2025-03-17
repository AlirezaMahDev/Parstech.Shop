namespace Parstech.Shop.Shared.DTOs;

public class ProductParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public string Store { get; set; }
    public string Categury { get; set; }
    public string Brand { get; set; }
    public string Type { get; set; }
}