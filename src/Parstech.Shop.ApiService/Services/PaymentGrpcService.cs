using Grpc.Core;

namespace Parstech.Shop.ApiService.Services;

public class PaymentGrpcService : PaymentService.PaymentServiceBase
{
    public override Task<PaymentStatusResponse> GetPaymentStatus(PaymentStatusRequest request,
        ServerCallContext context)
    {
        try
        {
            bool isSuccess = request.Status.ToLower() == "ok";

            return Task.FromResult(new PaymentStatusResponse
            {
                IsSuccessed = isSuccess, Message = isSuccess ? "عملیات پرداخت موفق است" : "عملیات پرداخت ناموفق است"
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new PaymentStatusResponse
            {
                IsSuccessed = false, Message = $"Error processing payment status: {ex.Message}"
            });
        }
    }
}