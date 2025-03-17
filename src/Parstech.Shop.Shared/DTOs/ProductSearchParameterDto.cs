namespace Parstech.Shop.Shared.DTOs;

public class ProductSearchParameterDto
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string Filter { get; set; }
    public string Store { get; set; }
    public string Categury { get; set; }
    public int CateguryId { get; set; }
    public long MinPrice { get; set; }
    public long MaxPrice { get; set; }
    public bool Exist { get; set; }

    public string Type { get; set; }
    public string IsActive { get; set; }
    public string Brand { get; set; }
}