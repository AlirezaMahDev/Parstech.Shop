using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class CouponAdminGrpcClient : GrpcClientBase
{
    private readonly CouponAdminService.CouponAdminServiceClient _client;

    public CouponAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new CouponAdminService.CouponAdminServiceClient(Channel);
    }

    // Coupon management operations
    public async Task<CouponPageingDto> GetCouponsForAdminAsync(int currentPage, int takePage, string filter = "")
    {
        var request = new CouponParameterRequest
        {
            CurrentPage = currentPage, TakePage = takePage, Filter = filter ?? string.Empty
        };

        return await _client.GetCouponsForAdminAsync(request);
    }

    public async Task<CouponDto> GetCouponByIdAsync(int couponId)
    {
        var request = new CouponRequest { CouponId = couponId };
        return await _client.GetCouponByIdAsync(request);
    }

    public async Task<CouponTypesResponse> GetCouponTypesAsync()
    {
        var request = new EmptyRequest();
        return await _client.GetCouponTypesAsync(request);
    }

    public async Task<ResponseDto> CreateCouponAsync(CouponDto coupon)
    {
        return await _client.CreateCouponAsync(coupon);
    }

    public async Task<ResponseDto> UpdateCouponAsync(CouponDto coupon)
    {
        return await _client.UpdateCouponAsync(coupon);
    }

    public async Task<ResponseDto> DeleteCouponAsync(int couponId)
    {
        var request = new CouponRequest { CouponId = couponId };
        return await _client.DeleteCouponAsync(request);
    }
}