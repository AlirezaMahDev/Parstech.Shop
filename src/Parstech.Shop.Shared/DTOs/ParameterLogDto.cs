namespace Parstech.Shop.Shared.DTOs;

public class ParameterLogDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public int ProductId { get; set; }
}