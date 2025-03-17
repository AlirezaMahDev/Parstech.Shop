namespace Parstech.Shop.Shared.DTOs;

public class OrderShippingDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ShippingTypeId { get; set; }
    public string ShippingType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? PostCode { get; set; }

    public string? FullAddress { get; set; }
}