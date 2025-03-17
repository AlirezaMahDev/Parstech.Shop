namespace Parstech.Shop.Shared.Models;

public partial class OrderShipping
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ShippingTypeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? PostCode { get; set; }

    public string? FullAddress { get; set; }

    public string? UserShippingId { get; set; }

    public string? DeliveryCode { get; set; }

    public string? StatusName { get; set; }

    public string? Description { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ShippingType ShippingType { get; set; } = null!;
}