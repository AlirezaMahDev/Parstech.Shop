namespace Parstech.Shop.Shared.DTOs;

public class UserFilterDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? EconomicCode { get; set; }
    public string? NationalCode { get; set; }
    public string? Mobile { get; set; }
}