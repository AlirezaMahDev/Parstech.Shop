using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.PaymentService;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class PaymentGrpcClient
    {
        private readonly PaymentService.PaymentServiceClient _client;

        public PaymentGrpcClient(GrpcChannel channel)
        {
            _client = new PaymentService.PaymentServiceClient(channel);
        }

        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string status)
        {
            var request = new PaymentStatusRequest { Status = status };
            return await _client.GetPaymentStatusAsync(request);
        }
    }
} 