namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductRepresentation
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public string UniqeCode { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public int TypeId { get; set; }

    public int RepresntationId { get; set; }

    public int ProductStockPriceId { get; set; }

    public int UserId { get; set; }

    public string? FileName { get; set; }

    public virtual ProductStockPrice ProductStockPrice { get; set; } = null!;

    public virtual Representation Represntation { get; set; } = null!;

    public virtual RepresentationType Type { get; set; } = null!;
}