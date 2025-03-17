using Grpc.Net.Client;

using Microsoft.AspNetCore.Identity.Data;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class UserAuthGrpcClient
{
    private readonly UserAuthService.UserAuthServiceClient _client;

    public UserAuthGrpcClient(GrpcChannel channel)
    {
        _client = new UserAuthService.UserAuthServiceClient(channel);
    }

    public async Task<LoginResponse> LoginAsync(string username, string password, bool rememberMe = true)
    {
        var request = new LoginRequest { Username = username, Password = password, RememberMe = rememberMe };

        return await _client.LoginAsync(request);
    }

    public async Task<string> ProtectDataAsync(string data, string purpose)
    {
        var request = new ProtectDataRequest { Data = data, Purpose = purpose };

        var response = await _client.ProtectDataAsync(request);
        return response.ProtectedData;
    }
}