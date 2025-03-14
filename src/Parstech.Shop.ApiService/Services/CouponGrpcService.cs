using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.CouponService;
using Shop.Application.Features.Coupon.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class CouponGrpcService : CouponService.CouponServiceBase
    {
        private readonly IMediator _mediator;
        
        public CouponGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<UseCouponResponse> UseCoupon(UseCouponRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new UseCouponQueryReq(request.OrderId, request.Code));
                
                return new UseCouponResponse
                {
                    Status = result.Status,
                    Message = result.Message,
                    Discount = result.Discount,
                    FinalPrice = result.FinalPrice
                };
            }
            catch (Exception ex)
            {
                return new UseCouponResponse
                {
                    Status = false,
                    Message = $"Error applying coupon: {ex.Message}"
                };
            }
        }
    }
} 