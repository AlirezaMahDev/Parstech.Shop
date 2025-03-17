namespace Parstech.Shop.Shared.DTOs;

public class variationRoot
{
    public int id { get; set; }
    public string sku { get; set; }
    public string price { get; set; }
    public string regular_price { get; set; }
    public string sale_price { get; set; }

    public string status { get; set; }


    public int stock_quantity { get; set; }
    public string stock_status { get; set; }

    public List<variationAttribute> attributes { get; set; }
}