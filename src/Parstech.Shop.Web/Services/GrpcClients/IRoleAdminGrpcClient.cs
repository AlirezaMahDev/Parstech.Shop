using Shop.Application.DTOs.IRole;
using Shop.Application.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IRoleAdminGrpcClient
    {
        // Role operations
        Task<List<IRoleDto>> GetAllRolesAsync();
        Task<ResponseDto> CreateRoleAsync(IRoleDto roleDto);
        Task<ResponseDto> UpdateRoleAsync(IRoleDto roleDto);
        Task<ResponseDto> DeleteRoleAsync(string roleId);
    }
} 