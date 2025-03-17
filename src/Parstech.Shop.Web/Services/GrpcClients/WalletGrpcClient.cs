using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class WalletGrpcClient
{
    private readonly WalletService.WalletServiceClient _client;

    public WalletGrpcClient(GrpcChannel channel)
    {
        _client = new WalletService.WalletServiceClient(channel);
    }

    public async Task<WalletResponse> GetWalletByUserIdAsync(int userId)
    {
        var request = new WalletRequest { UserId = userId };
        return await _client.GetWalletByUserIdAsync(request);
    }

    public async Task<TransactionResponse> GetActiveTransactionAsync(int walletId, string typeName)
    {
        var request = new TransactionRequest { WalletId = walletId, TypeName = typeName };

        return await _client.GetActiveTransactionAsync(request);
    }

    public async Task<CalculateResponse> CalculateInstallmentsAsync(long price, int transactionId, int month)
    {
        var request = new CalculateRequest { Price = price, TransactionId = transactionId, Month = month };

        return await _client.CalculateInstallmentsAsync(request);
    }

    public async Task<WalletTransactionResponse> GetWalletTransactionAsync(int transactionId)
    {
        var request = new WalletTransactionRequest { TransactionId = transactionId };
        return await _client.GetWalletTransactionAsync(request);
    }

    public async Task<WalletTransactionResponse> GetWalletTransactionByTokenAsync(string token)
    {
        var request = new WalletTransactionTokenRequest { Token = token };
        return await _client.GetWalletTransactionByTokenAsync(request);
    }

    public async Task<WalletTransactionUpdateResponse> UpdateWalletTransactionAsync(
        int transactionId,
        bool isSuccess,
        string trackingCode = null)
    {
        var request = new WalletTransactionUpdateRequest { TransactionId = transactionId, IsSuccess = isSuccess };

        if (!string.IsNullOrEmpty(trackingCode))
        {
            request.TrackingCode = trackingCode;
        }

        return await _client.UpdateWalletTransactionAsync(request);
    }

    public async Task<UserWalletResponse> GetUserWalletAsync(string userName)
    {
        var request = new UserWalletRequest { UserName = userName };
        return await _client.GetUserWalletAsync(request);
    }
}