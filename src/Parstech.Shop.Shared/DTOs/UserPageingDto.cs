namespace Parstech.Shop.Shared.DTOs;

public class UserPageingDto
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array UserDtos { get; set; }
}