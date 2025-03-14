using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.WalletService;

namespace Parstech.Shop.Web.Services.GrpcClients
{
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
            var request = new TransactionRequest
            {
                WalletId = walletId,
                TypeName = typeName
            };
            
            return await _client.GetActiveTransactionAsync(request);
        }

        public async Task<CalculateResponse> CalculateInstallmentsAsync(long price, int transactionId, int month)
        {
            var request = new CalculateRequest
            {
                Price = price,
                TransactionId = transactionId,
                Month = month
            };
            
            return await _client.CalculateInstallmentsAsync(request);
        }
    }
} 