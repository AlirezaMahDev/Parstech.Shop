using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.Wallet;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class WalletTransactionGrpcClient
    {
        private readonly WalletService.WalletServiceClient _client;

        public WalletTransactionGrpcClient(GrpcChannel channel)
        {
            _client = new WalletService.WalletServiceClient(channel);
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
            var request = new WalletTransactionUpdateRequest 
            { 
                TransactionId = transactionId,
                IsSuccess = isSuccess
            };
            
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
} 