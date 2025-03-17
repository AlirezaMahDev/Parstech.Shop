using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface IRoleAdminGrpcClient
{
    // Role operations
    Task<List<IRoleDto>> GetAllRolesAsync();
    Task<ResponseDto> CreateRoleAsync(IRoleDto roleDto);
    Task<ResponseDto> UpdateRoleAsync(IRoleDto roleDto);
    Task<ResponseDto> DeleteRoleAsync(string roleId);
}