using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services;

public class UserProfileGrpcClient
{
    private readonly UserProfileService.UserProfileServiceClient _client;

    public UserProfileGrpcClient(GrpcChannel channel)
    {
        _client = new UserProfileService.UserProfileServiceClient(channel);
    }

    public async Task<UserResponse> GetUserByUsernameAsync(string username)
    {
        var request = new UserByUsernameRequest { Username = username };
        return await _client.GetUserByUsernameAsync(request);
    }

    public async Task<UserInfoResponse> GetUserInfoAsync(UserInfoRequest request)
    {
        return await _client.GetUserInfoAsync(request);
    }

    public async Task<UserShippingListResponse> GetUserShippingAddressesAsync(int userId)
    {
        var request = new UserShippingRequest { UserId = userId };
        return await _client.GetUserShippingAddressesAsync(request);
    }

    public async Task<UserShippingResponse> GetUserShippingByIdAsync(int shippingId)
    {
        var request = new ShippingIdRequest { ShippingId = shippingId };
        return await _client.GetUserShippingByIdAsync(request);
    }

    public async Task<UserShippingResponse> CreateUserShippingAsync(
        int userId,
        string address,
        string postalCode,
        string mobile,
        string city,
        string province,
        bool isDefault)
    {
        var request = new CreateShippingRequest
        {
            UserId = userId,
            Address = address,
            PostalCode = postalCode,
            Mobile = mobile,
            City = city,
            Province = province,
            IsDefault = isDefault
        };

        return await _client.CreateUserShippingAsync(request);
    }

    public async Task<UserShippingResponse> UpdateUserShippingAsync(
        int id,
        int userId,
        string address,
        string postalCode,
        string mobile,
        string city,
        string province,
        bool isDefault)
    {
        var request = new UpdateShippingRequest
        {
            Id = id,
            UserId = userId,
            Address = address,
            PostalCode = postalCode,
            Mobile = mobile,
            City = city,
            Province = province,
            IsDefault = isDefault
        };

        return await _client.UpdateUserShippingAsync(request);
    }

    public async Task<DeleteShippingResponse> DeleteUserShippingAsync(int shippingId)
    {
        var request = new ShippingIdRequest { ShippingId = shippingId };
        return await _client.DeleteUserShippingAsync(request);
    }

    public async Task<UserOrdersResponse> GetUserOrdersHistoryAsync(int userId,
        int page,
        int pageSize,
        string searchTerm = "")
    {
        var request = new UserOrdersRequest
        {
            UserId = userId, Page = page, PageSize = pageSize, SearchTerm = searchTerm ?? string.Empty
        };

        return await _client.GetUserOrdersHistoryAsync(request);
    }

    public async Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId)
    {
        var request = new OrderDetailsRequest { OrderId = orderId };
        return await _client.GetOrderDetailsAsync(request);
    }

    public async Task<UserTransactionsResponse> GetUserTransactionsAsync(int walletId,
        int page,
        int pageSize,
        string transactionType = "")
    {
        var request = new UserTransactionsRequest
        {
            WalletId = walletId, Page = page, PageSize = pageSize, TransactionType = transactionType ?? string.Empty
        };

        return await _client.GetUserTransactionsAsync(request);
    }

    public async Task<TransactionDetailsResponse> GetTransactionDetailsAsync(int transactionId)
    {
        var request = new TransactionDetailsRequest { TransactionId = transactionId };
        return await _client.GetTransactionDetailsAsync(request);
    }
}