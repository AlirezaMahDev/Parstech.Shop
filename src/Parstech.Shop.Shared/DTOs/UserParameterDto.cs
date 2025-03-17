namespace Parstech.Shop.Shared.DTOs;

public class UserParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
}