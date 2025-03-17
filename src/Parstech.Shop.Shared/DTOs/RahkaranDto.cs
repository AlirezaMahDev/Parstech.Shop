namespace Parstech.Shop.Shared.DTOs;

public class RahkaranDto
{
    public string system { get; set; }
    public string orderCode { get; set; }
    public List<RakaranProductItem> Products { get; set; }
    public string Currency { get; set; }
    public string Customer { get; set; }
    public string Payertype { get; set; }
    public string SalesTypeID { get; set; }
    public string SalesOfficeID { get; set; }
    public string Plant { get; set; }
    public string Inventory { get; set; }
    public string SalesAreaID { get; set; }
    public string BrokerId { get; set; }
}