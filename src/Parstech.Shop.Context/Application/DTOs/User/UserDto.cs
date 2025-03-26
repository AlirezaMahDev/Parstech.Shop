namespace Parstech.Shop.Context.Application.DTOs.User;

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

public class UserPageingDto
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array UserDtos { get; set; }
}

public class UserParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }

}

public class UserInfoDto
{
    public string FullName { get; set; }
    public string Role { get; set; }
    public string LastLoginShamsi { get; set; }
    public string Position { get; set; }
}
public class UserNameDto
{
    public string userName { get; set; }
    public string Position { get; set; }
       
}

public class UserForSelectListDto
{
    public string UserName { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}

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