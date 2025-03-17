namespace Parstech.Shop.Shared.DTOs;

public class OrderShippingChangeDto
{
    public int OrderShippingId { get; set; }
    public List<UserShippingDto> UserShippings { get; set; }
}