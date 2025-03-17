using Grpc.Core;
using Grpc.Net.Client;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Protos.Admin;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class AuthAdminGrpcClient : IAuthAdminGrpcClient
{
    private readonly ILogger<AuthAdminGrpcClient> _logger;
    private readonly AuthAdminService.AuthAdminServiceClient _client;

    public AuthAdminGrpcClient(IConfiguration configuration, ILogger<AuthAdminGrpcClient> logger)
    {
        _logger = logger;
        GrpcChannel channel = GrpcChannel.ForAddress(configuration["GrpcSettings:ApiAddress"]);
        _client = new AuthAdminService.AuthAdminServiceClient(channel);
    }

    public async Task<Shared.DTOs.ResponseDto> LoginOrRegisterRequestAsync(string mobile)
    {
        try
        {
            var request = new LoginMobileRequest { Mobile = mobile };

            var response = await _client.LoginOrRegisterRequestAsync(request);

            // Map from proto DTO to application DTO
            return MapResponseDto(response);
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service LoginOrRegisterRequest");
            throw;
        }
    }

    public async Task<Shared.DTOs.ResponseDto> LoginByActiveCodeAsync(string mobile, string activeCode)
    {
        try
        {
            var request = new LoginActiveCodeRequest { Mobile = mobile, ActiveCode = activeCode };

            var response = await _client.LoginByActiveCodeAsync(request);

            // Map from proto DTO to application DTO
            return MapResponseDto(response);
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service LoginByActiveCode");
            throw;
        }
    }

    public async Task<Shared.DTOs.ResponseDto> LoginByPasswordAsync(string mobile, string password)
    {
        try
        {
            var request = new LoginPasswordRequest { Mobile = mobile, Password = password };

            var response = await _client.LoginByPasswordAsync(request);

            // Map from proto DTO to application DTO
            return MapResponseDto(response);
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service LoginByPassword");
            throw;
        }
    }

    public async Task<Shared.DTOs.ResponseDto> RegisterUserAsync(UserRegisterDto userRegister)
    {
        try
        {
            var request = new RegisterUserRequest
            {
                UserName = userRegister.UserName ?? string.Empty,
                FirstName = userRegister.FirstName ?? string.Empty,
                LastName = userRegister.LastName ?? string.Empty,
                NationalCode = userRegister.NationalCode ?? string.Empty,
                Country = userRegister.Country ?? string.Empty,
                State = userRegister.State ?? string.Empty,
                City = userRegister.City ?? string.Empty,
                Address = userRegister.Address ?? string.Empty,
                Mobile = userRegister.Mobile ?? string.Empty
            };

            var response = await _client.RegisterUserAsync(request);

            // Map from proto DTO to application DTO
            return MapResponseDto(response);
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service RegisterUser");
            throw;
        }
    }

    // Helper method to map response from proto to application DTO
    private Shared.DTOs.ResponseDto MapResponseDto(AuthResponseDto response)
    {
        return new Shared.DTOs.ResponseDto
        {
            IsSuccessed = response.IsSuccessed,
            Message = response.Message,
            Token = response.Token,
            RefreshToken = response.RefreshToken,
            NeedRegister = response.NeedRegister,
            NeedActiveCode = response.NeedActiveCode,
            NeedPassword = response.NeedPassword,
            NeedResendActiveCode = response.NeedResendActiveCode,
            ActiveRegister = response.ActiveRegister
            // Object property would require more complex mapping if needed
        };
    }
} 