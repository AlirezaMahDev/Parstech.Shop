namespace Parstech.Shop.Shared.DTOs;

public class PropertyParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public int categuryId { get; set; }
    public int propertyCateguryId { get; set; }
}