namespace Parstech.Shop.Context.Domain.Models;

public partial class UserShipping
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? PostCode { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
