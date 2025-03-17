namespace Parstech.Shop.Shared.DTOs;

public class BaseProductPropertyDto
{
    public int Id { get; set; }
    public string PropertyCategury { get; set; }
    public List<ProductPropertyDto> Properties { get; set; }
}