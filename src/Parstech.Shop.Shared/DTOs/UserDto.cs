namespace Parstech.Shop.Shared.DTOs;

public class UserDto
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;
    public string ActiveCode { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? FullName { get; set; }

    public string? UserId { get; set; }

    public string? Avatar { get; set; }

    public DateTime LastLoginDate { get; set; }
    public string LastLoginDateShamsi { get; set; }
    public string EconomicCode { get; set; }

    public bool SendSms { get; set; }

    public bool? IsDelete { get; set; }
}