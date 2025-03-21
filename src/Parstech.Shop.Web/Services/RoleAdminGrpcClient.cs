using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class RoleAdminGrpcClient : GrpcClientBase, IRoleAdminGrpcClient
{
    private readonly RoleAdminService.RoleAdminServiceClient _client;

    public RoleAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new RoleAdminService.RoleAdminServiceClient(Channel);
    }

    public async Task<List<IRoleDto>> GetAllRolesAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.GetAllRolesAsync(request);

        var result = new List<IRoleDto>();
        foreach (var role in response.Roles)
        {
            result.Add(new() { Id = role.Id, Name = role.Name, PersianName = role.PersianName });
        }

        return result;
    }

    public async Task<ResponseDto> CreateRoleAsync(IRoleDto roleDto)
    {
        var request = new RoleRequest { Id = roleDto.Id, Name = roleDto.Name, PersianName = roleDto.PersianName };

        var response = await _client.CreateRoleAsync(request);
        return new() { IsSuccessed = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> UpdateRoleAsync(IRoleDto roleDto)
    {
        var request = new RoleRequest { Id = roleDto.Id, Name = roleDto.Name, PersianName = roleDto.PersianName };

        var response = await _client.UpdateRoleAsync(request);
        return new() { IsSuccessed = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> DeleteRoleAsync(string roleId)
    {
        var request = new RoleIdRequest { Id = roleId };

        var response = await _client.DeleteRoleAsync(request);
        return new() { IsSuccessed = response.Status, Message = response.Message, Code = response.Code };
    }
}