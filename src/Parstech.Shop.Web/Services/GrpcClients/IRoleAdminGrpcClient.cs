using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public interface IRoleAdminGrpcClient
{
    // Role operations
    Task<List<IRoleDto>> GetAllRolesAsync();
    Task<ResponseDto> CreateRoleAsync(IRoleDto roleDto);
    Task<ResponseDto> UpdateRoleAsync(IRoleDto roleDto);
    Task<ResponseDto> DeleteRoleAsync(string roleId);
}