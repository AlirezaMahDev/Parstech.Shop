using Grpc.Core;
using Parstech.Shop.Shared.Protos.PaymentService;

namespace Shop.ApiService.Services
{
    public class PaymentGrpcService : PaymentService.PaymentServiceBase
    {
        public override Task<PaymentStatusResponse> GetPaymentStatus(PaymentStatusRequest request, ServerCallContext context)
        {
            try
            {
                var isSuccess = request.Status.ToLower() == "ok";
                
                return Task.FromResult(new PaymentStatusResponse
                {
                    IsSuccessed = isSuccess,
                    Message = isSuccess ? "عملیات پرداخت موفق است" : "عملیات پرداخت ناموفق است"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new PaymentStatusResponse
                {
                    IsSuccessed = false,
                    Message = $"Error processing payment status: {ex.Message}"
                });
            }
        }
    }
} 