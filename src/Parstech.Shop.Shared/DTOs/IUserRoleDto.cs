namespace Parstech.Shop.ApiService.Application.DTOs;

public class IUserRoleDto
{
    public int NumberuserId { get; set; }
    public string UserId { get; set; }
    public string RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? UserName { get; set; }
    public string? PersianRoleName { get; set; }
}