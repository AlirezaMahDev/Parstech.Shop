using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class BrandAdminGrpcClient : GrpcClientBase
{
    private readonly BrandAdminService.BrandAdminServiceClient _client;

    public BrandAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new BrandAdminService.BrandAdminServiceClient(Channel);
    }

    // Brand management operations
    public async Task<BrandPageingDto> GetBrandsForAdminAsync(int currentPage, int takePage, string filter = "")
    {
        var request = new BrandParameterRequest
        {
            CurrentPage = currentPage, TakePage = takePage, Filter = filter ?? string.Empty
        };

        return await _client.GetBrandsForAdminAsync(request);
    }

    public async Task<BrandDto> GetBrandAsync(int brandId)
    {
        var request = new BrandRequest { BrandId = brandId };
        return await _client.GetBrandAsync(request);
    }

    public async Task<ResponseDto> CreateBrandAsync(BrandDto brand)
    {
        return await _client.CreateBrandAsync(brand);
    }

    public async Task<ResponseDto> UpdateBrandAsync(BrandDto brand)
    {
        return await _client.UpdateBrandAsync(brand);
    }

    public async Task<ResponseDto> DeleteBrandAsync(int brandId)
    {
        var request = new BrandRequest { BrandId = brandId };
        return await _client.DeleteBrandAsync(request);
    }
}