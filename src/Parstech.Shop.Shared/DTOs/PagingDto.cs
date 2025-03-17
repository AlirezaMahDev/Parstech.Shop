namespace Parstech.Shop.Shared.DTOs;

public class PagingDto
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array List { get; set; }
}