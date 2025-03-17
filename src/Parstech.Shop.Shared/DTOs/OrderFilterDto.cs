namespace Parstech.Shop.Shared.DTOs;

public class OrderFilterDto
{
    public List<storeFilterDto> stores { get; set; }
    public List<statusFilterDto> statuses { get; set; }
    public List<payFilterDto> pays { get; set; }
    public List<ordercodeFilterDto> ordercodes { get; set; }
    public List<customerFilterDto> customers { get; set; }
}