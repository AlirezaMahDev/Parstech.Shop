namespace Parstech.Shop.Shared.DTOs;

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