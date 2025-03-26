namespace Parstech.Shop.Context.Application.DTOs.User;

public class UserRegisterDto
{
    public string UserName { get; set; } = null!;
    public string ActiveCode { get; set; } = null!;

    public string? Password { get; set; } = null!;
    public string? RePassword { get; set; } = null!;

    public string? IUserId { get; set; }
    public int UserId { get; set; }

    public string? Avatar { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Company { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Mobile { get; set; } = null!;

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? PostCode { get; set; }

    public string? EconomicCode { get; set; }

    public string? NationalCode { get; set; }

    public int NumberuserId { get; set; }

    public string? RoleName { get; set; }
}
public class RegisterDto
{
    public string CaptchaKey { get; set; }
    public string CaptchaValue { get; set; }
    public string Mobile { get; set; }
    public string Name { get; set; }

    public string Family { get; set; }

    public string MeliCode { get; set; } 

    public string State { get; set; }

    public string City { get; set; }

    public string Address { get; set; }
}