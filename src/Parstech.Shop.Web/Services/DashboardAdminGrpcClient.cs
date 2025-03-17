using Grpc.Core;
using Grpc.Net.Client;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class DashboardAdminGrpcClient : IDashboardAdminGrpcClient
{
    private readonly ILogger<DashboardAdminGrpcClient> _logger;
    private readonly DashboardAdminService.DashboardAdminServiceClient _client;

    public DashboardAdminGrpcClient(IConfiguration configuration, ILogger<DashboardAdminGrpcClient> logger)
    {
        _logger = logger;
        GrpcChannel channel = GrpcChannel.ForAddress(configuration["GrpcSettings:ApiAddress"]);
        _client = new DashboardAdminService.DashboardAdminServiceClient(channel);
    }

    public async Task<IndexCountsDto> GetDashboardCountsAsync()
    {
        try
        {
            var response = await _client.GetDashboardCountsAsync(new EmptyRequest());

            // Map from protobuf DTO to application DTO
            var result = new IndexCountsDto
            {
                Time = response.Time,
                UserCount = response.UserCount,
                ProductCount = response.ProductCount,
                IsLoadOrderCount = response.IsLoadOrderCount,
                AllTransactionsCount = response.AllTransactionsCount,
                CoinTransactionsCount = response.CoinTransactionsCount,
                WalletTransactionsCount = response.WalletTransactionsCount,
                FacilitiesTransactionsCount = response.FacilitiesTransactionsCount,
                PishFactorCount = response.PishFactorCount,
                SouratHesabCount = response.SouratHesabCount,
                RepresentationsProductsForChart = new(),
                RepresentationsProductsForMap = new()
            };

            // Map representation products for chart
            foreach (var item in response.RepresentationsProductsForChart)
            {
                result.RepresentationsProductsForChart.Add(new()
                {
                    RepresentationName = item.RepresentationName,
                    RepresentationProducts = item.RepresentationProducts
                });
            }

            // Map representation products for map
            foreach (var item in response.RepresentationsProductsForMap)
            {
                result.RepresentationsProductsForMap.Add(new()
                {
                    RepresentationName = item.RepresentationName,
                    latitude = item.Latitude,
                    longitude = item.Longitude,
                    RepresentationSells = item.RepresentationSells
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service GetDashboardCounts");
            throw;
        }
    }
}