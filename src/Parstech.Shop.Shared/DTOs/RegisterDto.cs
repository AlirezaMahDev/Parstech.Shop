namespace Parstech.Shop.Shared.DTOs;

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