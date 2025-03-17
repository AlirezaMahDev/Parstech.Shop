namespace Parstech.Shop.Shared.DTOs;

public class RahkaranAllDto
{
    public RahkaranOrderDto order { get; set; }
    public RahkaranUserDto customer { get; set; }
    public List<RahkaranProductDto> products { get; set; }
}