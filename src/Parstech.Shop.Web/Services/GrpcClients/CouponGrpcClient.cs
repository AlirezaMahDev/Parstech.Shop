using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class CouponGrpcClient
{
    private readonly CouponService.CouponServiceClient _client;

    public CouponGrpcClient(GrpcChannel channel)
    {
        _client = new CouponService.CouponServiceClient(channel);
    }

    public async Task<UseCouponResponse> UseCouponAsync(int orderId, string code)
    {
        var request = new UseCouponRequest { OrderId = orderId, Code = code };

        return await _client.UseCouponAsync(request);
    }
}