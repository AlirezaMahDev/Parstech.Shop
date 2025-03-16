using Microsoft.Extensions.Configuration;
using Shop.Application.DTOs.IRole;
using Shop.Application.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parstech.Shop.Shared.Protos.RoleAdmin;

namespace Parstech.Shop.Web.Services.GrpcClients
{
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
                result.Add(new IRoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    PersianName = role.PersianName
                });
            }
            
            return result;
        }
        
        public async Task<ResponseDto> CreateRoleAsync(IRoleDto roleDto)
        {
            var request = new RoleRequest
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                PersianName = roleDto.PersianName
            };
            
            var response = await _client.CreateRoleAsync(request);
            return new ResponseDto
            {
                IsSuccessed = response.Status,
                Message = response.Message,
                Code = response.Code
            };
        }
        
        public async Task<ResponseDto> UpdateRoleAsync(IRoleDto roleDto)
        {
            var request = new RoleRequest
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                PersianName = roleDto.PersianName
            };
            
            var response = await _client.UpdateRoleAsync(request);
            return new ResponseDto
            {
                IsSuccessed = response.Status,
                Message = response.Message,
                Code = response.Code
            };
        }
        
        public async Task<ResponseDto> DeleteRoleAsync(string roleId)
        {
            var request = new RoleIdRequest
            {
                Id = roleId
            };
            
            var response = await _client.DeleteRoleAsync(request);
            return new ResponseDto
            {
                IsSuccessed = response.Status,
                Message = response.Message,
                Code = response.Code
            };
        }
    }
} 