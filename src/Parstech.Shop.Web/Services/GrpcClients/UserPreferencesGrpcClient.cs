using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class UserPreferencesGrpcClient
{
    private readonly UserPreferencesService.UserPreferencesServiceClient _client;

    public UserPreferencesGrpcClient(GrpcChannel channel)
    {
        _client = new UserPreferencesService.UserPreferencesServiceClient(channel);
    }

    public async Task<UserBillingResponse> GetUserBillingAsync(int userId)
    {
        var request = new UserIdRequest { UserId = userId };
        return await _client.GetUserBillingAsync(request);
    }

    public async Task<UserBillingResponse> UpdateUserBillingAsync(
        int id,
        int userId,
        string companyName,
        string economicCode,
        string nationalId,
        string registrationNumber,
        string phoneNumber,
        string postalCode,
        string address)
    {
        var request = new UpdateBillingRequest
        {
            Id = id,
            UserId = userId,
            CompanyName = companyName ?? string.Empty,
            EconomicCode = economicCode ?? string.Empty,
            NationalId = nationalId ?? string.Empty,
            RegistrationNumber = registrationNumber ?? string.Empty,
            PhoneNumber = phoneNumber ?? string.Empty,
            PostalCode = postalCode ?? string.Empty,
            Address = address ?? string.Empty
        };

        return await _client.UpdateUserBillingAsync(request);
    }

    public async Task<StatusResponse> ChangePasswordAsync(string oldPassword,
        string newPassword,
        string confirmPassword)
    {
        var request = new ChangePasswordRequest
        {
            OldPassword = oldPassword, NewPassword = newPassword, ConfirmPassword = confirmPassword
        };

        return await _client.ChangePasswordAsync(request);
    }

    public async Task<FavoriteProductsResponse> GetFavoriteProductsAsync(string userName)
    {
        var request = new UserNameRequest { UserName = userName };
        return await _client.GetFavoriteProductsAsync(request);
    }

    public async Task<ComparisonProductsResponse> GetComparisonProductsAsync(string userName)
    {
        var request = new UserNameRequest { UserName = userName };
        return await _client.GetComparisonProductsAsync(request);
    }

    public async Task<ShoppingCartResponse> GetShoppingCartAsync(int userId)
    {
        var request = new UserIdRequest { UserId = userId };
        return await _client.GetShoppingCartAsync(request);
    }

    public async Task<double> GetWalletAmountAsync(int userId)
    {
        var request = new UserIdRequest { UserId = userId };
        var response = await _client.GetWalletAmountAsync(request);
        return response.Amount;
    }

    public async Task<double> GetWalletCoinAsync(int userId)
    {
        var request = new UserIdRequest { UserId = userId };
        var response = await _client.GetWalletCoinAsync(request);
        return response.Coin;
    }

    public async Task<double> GetWalletFacilitiesAsync(int userId)
    {
        var request = new UserIdRequest { UserId = userId };
        var response = await _client.GetWalletFacilitiesAsync(request);
        return response.Facilities;
    }
}