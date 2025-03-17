using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services;

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