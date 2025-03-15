using Shop.Application.DTOs.IUserRole;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.DTOs.UserShipping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IUserAdminGrpcClient
    {
        // User operations
        Task<UserPageingDto> GetUsersAsync(UserParameterDto parameter);
        Task<ResponseDto<UserDto>> GetUserAsync(int userId);
        Task<ResponseDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<ResponseDto<string>> LoginByUserAsync(int userId);
        
        // User Billing operations
        Task<ResponseDto<List<UserBillingDto>>> GetUserBillingsAsync(int userId);
        Task<ResponseDto<UserBillingDto>> GetUserBillingAsync(int billingId);
        Task<ResponseDto> CreateUserBillingAsync(CreateUserBillingDto createUserBillingDto);
        Task<ResponseDto> UpdateUserBillingAsync(UpdateUserBillingDto updateUserBillingDto);
        Task<ResponseDto> DeleteUserBillingAsync(int billingId);
        
        // User Shipping operations
        Task<ResponseDto<List<UserShippingDto>>> GetUserShippingsAsync(int userId);
        Task<ResponseDto<UserShippingDto>> GetUserShippingAsync(int shippingId);
        Task<ResponseDto> CreateUserShippingAsync(CreateUserShippingDto createUserShippingDto);
        Task<ResponseDto> UpdateUserShippingAsync(UpdateUserShippingDto updateUserShippingDto);
        Task<ResponseDto> DeleteUserShippingAsync(int shippingId);
        
        // User Role operations
        Task<ResponseDto<List<UserRoleDto>>> GetUserRolesAsync(int userId);
        Task<ResponseDto> AddUserRoleAsync(int userId, UserRoleDto userRoleDto);
        Task<ResponseDto> DeleteUserRoleAsync(int userId, UserRoleDto userRoleDto);
    }
} 